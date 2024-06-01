using System;
using UnityEngine;

namespace Mc
{
    public class Queue<T> : MonoBehaviour
    {
        public LinkedList<T> list = new LinkedList<T>();

        public int Count
        {
            get { return list.Count; }
        }

        public void Enqueue(T data)
        {
            list.InsertLast(data);
        }

        // LinkedList의 헤드 부분을 삭제, insert는 last에서 이뤄지므로 head 부분을 삭제하면 선입선출 방식을 완성할 수 있음
        public T Dequeue()
        {
            if (list.head != null)
            {
                T returnData = list.head.data;
                list.DeleteNode(list.head.data);

                return returnData;
            }
            else
            {
                throw new InvalidOperationException("Queue is empty");
            }
        }
    }
}