/*
    Для каждого токена в выражении:
    Если токен — операнд:
        добавить в выходной список
    Если токен — оператор:
        пока на вершине стека оператор с >= приоритетом:
            извлечь из стека в выходной список
        положить текущий оператор в стек
    Если токен — "(":
        положить в стек
    Если токен — ")":
        пока вершина стека не "(", извлекать в выход
        удалить "(" из стека

    После обработки всех токенов:
        извлечь оставшиеся операторы из стека в выход
*/

class InfixToPostfix
{
    static int GetPrecedence(string op)
    {
        return op switch
        {
            "u-" => 4,       // унарный минус — самый высокий приоритет
            "^" => 3,
            "*" or "/" => 2,
            "+" or "-" => 1,
            _ => 0
        };
    }

    static bool IsLeftAssociative(string op)
    {
        return op != "^" && op != "u-"; // унарный минус и ^ — правосторонние
    }

    static bool IsOperator(string token)
    {
        return token == "+" || token == "-" || token == "*" || token == "/" || token == "^" || token == "u-";
    }

    public static string ConvertToPostfix(string expression)
    {
        Stack<string> operators = new();
        List<string> output = new();
        string[] tokens = expression.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        string prevToken = null;

        foreach (var token in tokens)
        {
            string currentToken = token;

            // Определение унарного минуса
            if (currentToken == "-" && (prevToken == null || prevToken == "(" || IsOperator(prevToken)))
            {
                currentToken = "u-"; // заменим на унарный минус
            }

            if (double.TryParse(currentToken, out _) || char.IsLetter(currentToken[0]))
            {
                output.Add(currentToken);
            }
            else if (IsOperator(currentToken))
            {
                while (operators.Count > 0 && IsOperator(operators.Peek()))
                {
                    string top = operators.Peek();
                    if ((IsLeftAssociative(currentToken) && GetPrecedence(currentToken) <= GetPrecedence(top)) ||
                        (!IsLeftAssociative(currentToken) && GetPrecedence(currentToken) < GetPrecedence(top)))
                    {
                        output.Add(operators.Pop());
                    }
                    else break;
                }
                operators.Push(currentToken);
            }
            else if (currentToken == "(")
            {
                operators.Push(currentToken);
            }
            else if (currentToken == ")")
            {
                while (operators.Count > 0 && operators.Peek() != "(")
                {
                    output.Add(operators.Pop());
                }
                if (operators.Count == 0)
                    throw new ArgumentException("Неверная расстановка скобок");
                operators.Pop(); // удалить "("
            }

            prevToken = currentToken;
        }

        while (operators.Count > 0)
        {
            if (operators.Peek() == "(" || operators.Peek() == ")")
                throw new ArgumentException("Неверная расстановка скобок");
            output.Add(operators.Pop());
        }

        return string.Join(" ", output);
    }

    static void Main()
    {
        Console.WriteLine("Введите выражение в инфиксной форме (разделяйте пробелами):");
        string input = Console.ReadLine(); // Пример: -A + B * ( C - -D )
        try
        {
            string postfix = ConvertToPostfix(input);
            Console.WriteLine("Постфиксная форма: " + postfix);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }
}

