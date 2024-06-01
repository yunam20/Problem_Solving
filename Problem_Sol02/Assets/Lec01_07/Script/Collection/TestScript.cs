using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Mc.Stack<int> testStack;
    public Mc.Queue<int> testQueue;

    int CalculateGenerate(int num)
    {
        // �Էµ� ���ڸ� ���ڿ��� ��ȯ�Ͽ� �� �ڸ����� �и�
        string numStr = num.ToString();
        int digitSum = 0;

        // �� �ڸ����� �� ���
        foreach (char digitChar in numStr)
        {
            digitSum += (int)Char.GetNumericValue(digitChar);
        }

        // ���� ���ڿ� �� �ڸ����� ���� ���Ͽ� ��ȯ
        return num + digitSum;
    }

    // Start is called before the first frame update
    void Start()
    {
        List<int> value = new List<int>();

        // CalculateGenerate �Լ��� ����Ͽ� value ����Ʈ �ʱ�ȭ
        for (int i = 0; i <= 5000; i++) // 0���� 100������ ���ڷ� ����
        {
            value.Add(CalculateGenerate(i));
        }

        // value ����Ʈ�� �� ��� ���
        foreach (int item in value)
        {
            Debug.Log(item);
        }

        int all = 0;

        // value ����Ʈ�� �� ��� �߿��� CalculateGenerate �Լ��� ���� �� ���� ���� �����Ͽ� ���
        for (int i = 0; i <= value.Count; i++) // value.Count�� ����
        {
            if (!value.Contains(i))
            {
                all += i;
            }
        }

        Debug.Log("��: " + all);

        /*
        testStack = new Mc.Stack<int>();
        testQueue = new Mc.Queue<int>();

        testStack.Push(1);
        testStack.Push(2);
        testStack.Push(3);
        Debug.Log("Stack: " + testStack.Pop());
        Debug.Log("Stack: " + testStack.Pop());
        Debug.Log("Stack: " + testStack.Pop());

        testQueue.Enqueue(1);
        testQueue.Enqueue(2);
        testQueue.Enqueue(3);
        Debug.Log("Queue: " + testQueue.Dequeue());
        Debug.Log("Queue: " + testQueue.Dequeue());
        Debug.Log("Queue: " + testQueue.Dequeue());
        */
    }
}
