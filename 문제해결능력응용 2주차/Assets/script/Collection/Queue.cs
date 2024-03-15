using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue : MonoBehaviour
{
    // 총알 프리펩
    public GameObject BulletPrefab;

    // 총알 Linked List 만들기
    BulletLinkedList LinkedList;

    // 총알 Linked List
    class BulletLinkedList
    {
        Queue<Node> BulletQueue = new Queue<Node>();
        Node queueBackNode;

        GameObject prefabObj;
        int NodeCount;

        public BulletLinkedList(GameObject prefab, int Count)
        {
            prefabObj = prefab;
            NodeCount = 0;
            
            for (int i = 0; i < Count; i++)
            {
                Enqueue(i);
            }
            
        }

        void Enqueue(int Data)
        {
            if(NodeCount == 0)
            {
                BulletQueue.Enqueue(new Node(Instantiate(prefabObj), Data));
                queueBackNode = BulletQueue.Peek();
            }
            else
            {
                Node newNode = new Node(Instantiate(prefabObj), Data);
                BulletQueue.Enqueue(newNode);
                queueBackNode.Next = newNode;
                queueBackNode = newNode;
            }

            NodeCount++;
        }

        public void Dequeue()
        {
            if (NodeCount != 0)
            {
                GameObject dequeueBullet = BulletQueue.Dequeue().nodeBullet;
                dequeueBullet.SetActive(true);
                dequeueBullet.transform.position = new Vector3(-10, 1, 0);
            }
            else return;

            NodeCount--;
        }

        public void DebugNode()
        {
            Node currentNode = BulletQueue.Peek();

            while (true)
            {
                Debug.Log(currentNode.Data);
                if (currentNode.Next != null) currentNode = currentNode.Next;
                else break;
            }
        }
    }

    class Node
    {
        public GameObject nodeBullet;

        public Node Next;
        public int Data;

        public Node(GameObject obj, int Num)
        {
            nodeBullet = obj;
            Next = null;
            Data = Num;
            obj.SetActive(false);
        }
    }

    void Start()
    {
        LinkedList = new BulletLinkedList(BulletPrefab, 10);
        LinkedList.DebugNode();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LinkedList.Dequeue();
        }
    }
}
