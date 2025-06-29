public class Node
{
    public int Value;
    public Node Left;
    public Node Right;

    public Node(int value)
    {
        Value = value;
        Left = Right = null;
    }
}

public class BST
{
    public Node Root;

    public BST()
    {
        Root = null;
    }

    // ======== ВСТАВКА ========
    public void Insert(int value)
    {
        Root = InsertRecursive(Root, value);
    }

    private Node InsertRecursive(Node node, int value)
    {
        if (node == null)
        {
            return new Node(value);
        }

        if (value < node.Value)
            node.Left = InsertRecursive(node.Left, value);
        else if (value > node.Value)
            node.Right = InsertRecursive(node.Right, value);
        // Если значение уже существует, не вставляем его

        return node;
    }

    // ======== ПОИСК ========
    public bool Search(int value)
    {
        return SearchRecursive(Root, value);
    }

    private bool SearchRecursive(Node node, int value)
    {
        if (node == null)
            return false;

        if (value == node.Value)
            return true;
        else if (value < node.Value)
            return SearchRecursive(node.Left, value);
        else
            return SearchRecursive(node.Right, value);
    }

    // ======== УДАЛЕНИЕ ========
    public void Delete(int value)
    {
        Root = DeleteRecursive(Root, value);
    }

    private Node DeleteRecursive(Node node, int value)
    {
        if (node == null)
            return null;

        if (value < node.Value)
        {
            node.Left = DeleteRecursive(node.Left, value);
        }
        else if (value > node.Value)
        {
            node.Right = DeleteRecursive(node.Right, value);
        }
        else
        {
            // Найден узел
            if (node.Left == null && node.Right == null)
            {
                return null; // Без потомков
            }
            else if (node.Left == null)
            {
                return node.Right;
            }
            else if (node.Right == null)
            {
                return node.Left;
            }
            else
            {
                // Два потомка: найдём минимальное значение в правом поддереве
                int minValue = FindMin(node.Right);
                node.Value = minValue;
                node.Right = DeleteRecursive(node.Right, minValue);
            }
        }

        return node;
    }

    private int FindMin(Node node)
    {
        while (node.Left != null)
            node = node.Left;
        return node.Value;
    }

    // ======== ОБХОДЫ ========

    // Инфиксный (In-order): Left -> Node -> Right
    public void InOrderTraversal()
    {
        Console.Write("In-order: ");
        InOrderRecursive(Root);
        Console.WriteLine();
    }

    private void InOrderRecursive(Node node)
    {
        if (node != null)
        {
            InOrderRecursive(node.Left);
            Console.Write($"{node.Value} ");
            InOrderRecursive(node.Right);
        }
    }

    // Префиксный (Pre-order): Node -> Left -> Right
    public void PreOrderTraversal()
    {
        Console.Write("Pre-order: ");
        PreOrderRecursive(Root);
        Console.WriteLine();
    }

    private void PreOrderRecursive(Node node)
    {
        if (node != null)
        {
            Console.Write($"{node.Value} ");
            PreOrderRecursive(node.Left);
            PreOrderRecursive(node.Right);
        }
    }

    // Постфиксный (Post-order): Left -> Right -> Node
    public void PostOrderTraversal()
    {
        Console.Write("Post-order: ");
        PostOrderRecursive(Root);
        Console.WriteLine();
    }

    private void PostOrderRecursive(Node node)
    {
        if (node != null)
        {
            PostOrderRecursive(node.Left);
            PostOrderRecursive(node.Right);
            Console.Write($"{node.Value} ");
        }
    }
}
