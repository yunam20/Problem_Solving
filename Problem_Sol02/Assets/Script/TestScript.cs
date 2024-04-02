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
        // 입력된 숫자를 문자열로 변환하여 각 자릿수를 분리
        string numStr = num.ToString();
        int digitSum = 0;

        // 각 자릿수의 합 계산
        foreach (char digitChar in numStr)
        {
            digitSum += (int)Char.GetNumericValue(digitChar);
        }

        // 원래 숫자와 각 자릿수의 합을 더하여 반환
        return num + digitSum;
    }

    // Start is called before the first frame update
    void Start()
    {
        List<int> value = new List<int>();

        // CalculateGenerate 함수를 사용하여 value 리스트 초기화
        for (int i = 0; i <= 5000; i++) // 0부터 100까지의 숫자로 수정
        {
            value.Add(CalculateGenerate(i));
        }

        // value 리스트의 각 요소 출력
        foreach (int item in value)
        {
            Debug.Log(item);
        }

        int all = 0;

        // value 리스트의 각 요소 중에서 CalculateGenerate 함수로 구할 수 없는 값을 누적하여 계산
        for (int i = 0; i <= value.Count; i++) // value.Count로 수정
        {
            if (!value.Contains(i))
            {
                all += i;
            }
        }

        Debug.Log("합: " + all);

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
