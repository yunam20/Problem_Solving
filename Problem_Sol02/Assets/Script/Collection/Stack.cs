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

        // LinkedList의 헤드 부분을 삭제, insert는 front에서 이뤄지므로 head 부분을 삭제하면 선입후출 방식을 완성할 수 있음
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