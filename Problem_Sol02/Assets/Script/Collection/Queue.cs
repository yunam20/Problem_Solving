using System;
using UnityEngine;

namespace Mc
{
    public class Queue<T> : MonoBehaviour
    {
        LinkedList<T> list = new LinkedList<T>();

        public void Enqueue(T data)
        {
            list.InsertLast(data);
        }

        // LinkedList�� ��� �κ��� ����, insert�� last���� �̷����Ƿ� head �κ��� �����ϸ� ���Լ��� ����� �ϼ��� �� ����
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