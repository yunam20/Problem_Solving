using System;
using UnityEngine;

namespace Mc
{
    public class Stack<T> : MonoBehaviour
    {
        Queue<T> queue = new Queue<T>();
        Queue<T> queue2 = new Queue<T>();

        public void Push(T data)
        {
            while (queue.list.head != null)
            {
                queue2.Enqueue(queue.Dequeue());
            }

            queue.Enqueue(data);

            while (queue2.list.head != null)
            {
                queue.Enqueue(queue2.Dequeue());
            }
        }

        // LinkedList�� ��� �κ��� ����, insert�� front���� �̷����Ƿ� head �κ��� �����ϸ� �������� ����� �ϼ��� �� ����
        public T Pop()
        {
            return queue.Dequeue();
        }
    }
}