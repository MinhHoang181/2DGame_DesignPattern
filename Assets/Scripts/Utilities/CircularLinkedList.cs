public class CircularLinkedList<T> : ICircularLinkedList<T>
{
    public class Node
    {
        public T Value { get; set; }
        public Node Next { get; set; }
        public Node Prev { get; set; }

        public Node(T value, Node next, Node prev)
        {
            Value = value;
            Next = next;
            Prev = prev;
        }
    }

    public Node Head { get; set; }
    public Node Current { get; set; }

    public CircularLinkedList()
    {
        Head = null;
        Current = Head;
    }

    public CircularLinkedList(T[] values)
    {
        Head = null;
        Current = Head;
        Append(values);
    }

    public void Append(T value)
    {
        if (Head == null)
        {
            Head = new Node(value, null, null);
            Current = Head;
            return;
        }

        Node current = Head;

        while (current.Next != Head && current.Next != null)
        {
            current = current.Next;
        }

        current.Next = new Node(value, Head, current);
        Head.Prev = current.Next;
    }

    public void Append(T[] values)
    {
        int empty = 0;

        if (Head == null)
        {
            Head = new Node(values[0], null, null);
            Current = Head;
            empty = 1;
        }

        Node current = Head;

        while (current.Next != Head && current.Next != null)
        {
            current = current.Next;
        }

        for (int i = empty; i < values.Length; i++)
        {
            current.Next = new Node(values[i], Head, current);
            Head.Prev = current.Next;

            current = current.Next;
        }
    }

    public void NextNode()
    {
        if (Current.Next != null) Current = Current.Next;
    }

    public void PrevNode()
    {
        if (Current.Prev != null) Current = Current.Prev;
    }

    public void RevertToDefault()
    {
        if (Head != null) Current = Head;
    }
}
