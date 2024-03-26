using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Mc.Stack<int> testStack;
    public Mc.Queue<int> testQueue;

    // Start is called before the first frame update
    void Start()
    {
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
    }
}
