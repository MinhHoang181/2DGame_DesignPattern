public interface ICircularLinkedList<T>
{   
    void Append(T value);
    void Append(T[] value);
    void NextNode();
    void PrevNode();
    void RevertToDefault();
}
