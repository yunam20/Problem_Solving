using System;
using UnityEngine;

namespace Mc
{
    public class Stack<T> : MonoBehaviour
    {
        Queue<T> queue = new Queue<T>();

        public void Push(T data)
        {
            queue.Enqueue(data);
        }

        // LinkedList�� ��� �κ��� ����, insert�� front���� �̷����Ƿ� head �κ��� �����ϸ� �������� ����� �ϼ��� �� ����
        public T Pop()
        {
            Queue<T> queue2 = new Queue<T>();

            while (queue.Count > 1)
            {
                queue2.Enqueue(queue.Dequeue());
            }
            while (queue2.Count > 0)
            {
                queue.Enqueue(queue2.Dequeue());
            }

            return queue.Dequeue();
        }
    }
}