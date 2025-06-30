public class Stack<T>
{
    private T[] items;
    private int top;
    private int capacity;

    public Stack(int size = 10)
    {
        capacity = size;
        items = new T[capacity];
        top = -1;
    }

    public void Push(T item)
    {
        if (IsFull())
        {
            Resize();
        }
        items[++top] = item;
    }

    public T Pop()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Стек пуст.");

        return items[top--];
    }

    public T Peek()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Стек пуст.");

        return items[top];
    }

    public bool IsEmpty()
    {
        return top == -1;
    }

    public bool IsFull()
    {
        return top == capacity - 1;
    }

    private void Resize()
    {
        capacity *= 2;
        T[] newArray = new T[capacity];
        Array.Copy(items, newArray, items.Length);
        items = newArray;
    }

    public int Count => top + 1;
}
