using System;
using UnityEngine;

public class Node
{
    internal int data;
    internal Node next;
    public Node(int data)
    {
        this.data = data;
        next = null;
    }
}

public class LinkedList
{
    Node head;
    internal void InsertFront(int data)
    {
        Node node = new Node(data);
        node.next = head;
        head = node;
    }

    internal void InsertLast(int data)
    {
        Node node = new Node(data);
        if (head == null)
        {
            head = node;
            return;
        }
        Node lastNode = GetLastNode();
        lastNode.next = node;
    }

    internal Node GetLastNode()
    {
        Node temp = head;
        while (temp.next != null)
        {
            temp = temp.next;
        }
        return temp;
    }

    // prev �ڿ� data�� ���� ��带 �����ϱ�
    internal void InsertAfter(int prev, int data)
    {
        Node prevNode = null;

        // find prev
        for (Node temp = head; temp != null; temp = temp.next)
            if (temp.data == prev)
                prevNode = temp;

        if (prevNode == null)
        {
            Console.WriteLine("{0} data is not in the list");
            return;
        }
        Node node = new Node(data);
        node.next = prevNode.next;
        prevNode.next = node;
    }

    // key ���� �����ϰ� �ִ� ��带 �����ϱ�
    internal void DeleteNode(int key)
    {
        Node temp = head;
        Node prev = null;
        if (temp != null && temp.data == key) // head�� ã�� ���̸�
        {
            head = temp.next;
            return;
        }
        while (temp != null && temp.data != key)
        {
            prev = temp;
            temp = temp.next;
        }
        if (temp == null) // ������ ã�� ���� ������
        {
            return;
        }
        prev.next = temp.next;
    }

    internal void Reverse()
    {
        Node prev = null;
        Node current = head;
        Node temp = null;
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
        for (Node node = head; node != null; node = node.next)
            Console.Write(node.data + " -> ");
        Console.WriteLine();
    }
}

public class Queue
{
    public Queue()
    {

    }

    public void Pop()
    {

    }

    public void Push()
    {

    }

    public void Size()
    {

    }

    public void Empty()
    {

    }
}

