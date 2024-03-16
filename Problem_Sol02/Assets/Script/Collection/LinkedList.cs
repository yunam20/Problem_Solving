using UnityEngine;

namespace Mc
{
    public class LinkedList<T>
    {
        public Node<T> head;

        internal void InsertFront(T data)
        {
            Node<T> node = new Node<T>(data);
            node.next = head;
            head = node;
        }

        internal void InsertLast(T data)
        {
            Node<T> node = new Node<T>(data);
            if (head == null)
            {
                head = node;
                return;
            }
            Node<T> lastNode = GetLastNode();
            lastNode.next = node;
        }

        internal Node<T> GetLastNode()
        {
            Node<T> temp = head;
            while (temp.next != null)
            {
                temp = temp.next;
            }
            return temp;
        }

        // prev 뒤에 data를 갖는 노드를 삽입하기
        internal void InsertAfter(T prev, T data)
        {
            Node<T> prevNode = null;

            // find prev
            for (Node<T> temp = head; temp != null; temp = temp.next)
                if (temp.data.Equals(prev))
                    prevNode = temp;

            if (prevNode == null)
            {
                Debug.Log(prev + " data is not in the list");
                return;
            }
            Node<T> node = new Node<T>(data);
            node.next = prevNode.next;
            prevNode.next = node;
        }

        // key 값을 저장하고 있는 노드를 삭제하기
        internal void DeleteNode(T key)
        {
            Node<T> temp = head;
            Node<T> prev = null;
            if (temp != null && temp.data.Equals(key)) // head가 찾는 값이면
            {
                head = temp.next;
                return;
            }
            while (temp != null && !temp.data.Equals(key))
            {
                prev = temp;
                temp = temp.next;
            }
            if (temp == null) // 끝까지 찾는 값이 없으면
            {
                return;
            }
            prev.next = temp.next;
        }

        internal void Reverse()
        {
            Node<T> prev = null;
            Node<T> current = head;
            Node<T> temp = null;
            while (current != null)
            {
                temp = current.next;
                current.next = prev;
                prev = current;
                current = temp;
            }
            head = prev;
        }

        internal void Print()
        {
            for (Node<T> node = head; node != null; node = node.next)
                Debug.Log(node.data);
        }
    }
}