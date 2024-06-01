namespace Mc
{
    public class Node<T>
    {
        internal T data;
        internal Node<T> next;
        public Node(T data)
        {
            this.data = data;
            next = null;
        }
    }
}