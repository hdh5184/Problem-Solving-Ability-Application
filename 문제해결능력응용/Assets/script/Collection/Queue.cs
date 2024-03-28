using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

namespace DataStrucuture
{
    public class LinkedListNode<T>
    {
        public T Data { get; set; }
        public LinkedListNode<T> Next { get; set; }

        public LinkedListNode(T data)
        {
            Data = data;
            Next = null;
        }
    }

    public class LinkedList<T>
    {
        public LinkedListNode<T> head;
        public LinkedListNode<T> foot;

        public LinkedList()
        {
            head = null;
            foot = null;
        }

        public void Add(T data)
        {
            LinkedListNode<T> newNode = new LinkedListNode<T>(data);
            if (head == null)
            {
                head = newNode;
                foot = newNode;
            }
            else
            {
                //LinkedListNode<T> current = head;
                foot.Next = newNode;
                foot = newNode;
            }
        }
    }

    public class Queue<T>
    {
        private LinkedList<T> list;
        public int count;

        public Queue()
        {
            list = new LinkedList<T>();
            count = 0;
        }

        // 큐에 Node 추가
        public void Enqueue(T data)
        {
            list.Add(data);
            count++;
        }

        // 큐에서 Node 제거 후 data 반환
        public T Dequeue()
        {
            if (list.head == null)
            {
                throw new InvalidOperationException("Queue is empty.");
            }
            count--;

            T data = list.head.Data;
            LinkedListNode<T> newHead = list.head.Next;
            list.head.Next = null;
            list.head = newHead;
            return data;
        }

    }

    public class Stack<T>
    {
        // Queue 이용
        private Queue<T> queue;

        public Stack()
        {
            queue = new Queue<T>();
        }

        public void Push(T data)
        {
            queue.Enqueue(data);
        }

        // Queue의 foot Node의 data 반환
        public T Pop()
        {
            Queue<T> tempQueue = new Queue<T>(); // 임시 Queue

            // 마지막(foot) 이전까지의 Node를 임시 저장
            while (true)
            {
                if (queue.count == 1) break;
                tempQueue.Enqueue(queue.Dequeue());
            }

            // foot Node의 data 반환
            // Queue 갱신
            T data = queue.Dequeue();
            queue = tempQueue;
            return data;
        }
    }
}