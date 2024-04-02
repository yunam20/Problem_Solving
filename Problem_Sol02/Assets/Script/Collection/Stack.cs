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

        // LinkedList의 헤드 부분을 삭제, insert는 front에서 이뤄지므로 head 부분을 삭제하면 선입후출 방식을 완성할 수 있음
        public T Pop()
        {
            return queue.Dequeue();
        }
    }
}