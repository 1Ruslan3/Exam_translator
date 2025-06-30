public class ExprNode
{
    public string Value;
    public ExprNode Left;
    public ExprNode Right;

    public ExprNode(string value)
    {
        Value = value;
    }
}

public class ExpressionTreeBuilder
{
    private static int Precedence(string op)
    {
        return op switch
        {
            "+" or "-" => 1,
            "*" or "/" => 2,
            _ => 0
        };
    }

    private static bool IsOperator(string token) => "+-*/".Contains(token);

    public static List<string> ToPostfix(string expression)
    {
        var output = new List<string>();
        var operators = new Stack<string>();

        var tokens = Tokenize(expression);

        foreach (var token in tokens)
        {
            if (double.TryParse(token, out _))
            {
                output.Add(token);
            }
            else if (IsOperator(token))
            {
                while (operators.Count > 0 && Precedence(operators.Peek()) >= Precedence(token))
                {
                    output.Add(operators.Pop());
                }
                operators.Push(token);
            }
            else if (token == "(")
            {
                operators.Push(token);
            }
            else if (token == ")")
            {
                while (operators.Count > 0 && operators.Peek() != "(")
                {
                    output.Add(operators.Pop());
                }
                operators.Pop(); // Remove "("
            }
        }

        while (operators.Count > 0)
        {
            output.Add(operators.Pop());
        }

        return output;
    }

    private static List<string> Tokenize(string expr)
    {
        var tokens = new List<string>();
        var number = "";

        foreach (char c in expr)
        {
            if (char.IsDigit(c) || c == '.')
            {
                number += c;
            }
            else
            {
                if (number != "")
                {
                    tokens.Add(number);
                    number = "";
                }
                if (!char.IsWhiteSpace(c))
                    tokens.Add(c.ToString());
            }
        }

        if (number != "")
            tokens.Add(number);

        return tokens;
    }

    public static ExprNode BuildTree(string expression)
    {
        var postfix = ToPostfix(expression);
        var stack = new Stack<ExprNode>();

        foreach (var token in postfix)
        {
            if (IsOperator(token))
            {
                var right = stack.Pop();
                var left = stack.Pop();
                var node = new ExprNode(token)
                {
                    Left = left,
                    Right = right
                };
                stack.Push(node);
            }
            else
            {
                stack.Push(new ExprNode(token));
            }
        }

        return stack.Pop();
    }

    // Дополнительно: печать дерева в инфиксной форме
    public static void PrintInOrder(ExprNode node)
    {
        if (node == null) return;

        bool isOperator = IsOperator(node.Value);
        if (isOperator) Console.Write("(");
        PrintInOrder(node.Left);
        Console.Write($" {node.Value} ");
        PrintInOrder(node.Right);
        if (isOperator) Console.Write(")");
    }
}
