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
        Queue<Node> BulletQueue = new Queue<Node>(); // 총알 노드 저장
        Node queueBackNode; // Queue의 가장 뒤에 있는 요소

        GameObject prefabObj;   // 총알 Linked List 생성 시 지정할 프리펩 오브젝트
        int NodeCount;          // 노드 카운트

        // 생성자 및 초기화 (프리펩 오브젝, 추가할 노드 개수)
        public BulletLinkedList(GameObject prefab, int Count)
        {
            prefabObj = prefab;
            NodeCount = 0;
            
            for (int i = 0; i < Count; i++)
            {
                Enqueue(i);
            }
            
        }

        // 노드 추가 및 Queue 넣기	
        void Enqueue(int Data)
        {
            if(NodeCount == 0) // 처음 노드 생성하는 경우
            {
                // Queue에 새로 생성한 (데이터 들어간) 노드 넣기
                BulletQueue.Enqueue(new Node(Instantiate(prefabObj), Data));

                // 현재 노드가 1개만 있으니 Queue의 가장 뒤?에 있는 요소가 됨 어쨌든
                queueBackNode = BulletQueue.Peek(); 
            }
            else // 노드가 1개 이상이 있는 경우
            {
                // 새로운 노드 생성 및 임시 저장
                Node newNode = new Node(Instantiate(prefabObj), Data);
                BulletQueue.Enqueue(newNode);

                // 가장 나중에 넣은 노드를 새로 생성한 노드에 연결함
                queueBackNode.Next = newNode;

                // 새로 생성한 노드를 Queue의 가장 뒤?에 있는 요소로 지정
                queueBackNode = newNode;
            }

            NodeCount++; // 노드 개수 증가
        }

        // 노드 빼기 및 Queue 빼기
        public void Dequeue()
        {
            if (NodeCount != 0) // node가 남아 있으면
            {
                GameObject dequeueBullet = BulletQueue.Dequeue().nodeBullet;
                dequeueBullet.SetActive(true);
                dequeueBullet.transform.position = new Vector3(-10, 1, 0);
            }
            else return; //node가 남아 있지 않으면

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
        // 노드와 함께 생성한 오브젝트 연결
        public GameObject nodeBullet;

        public Node Next; // 다음 노드 연결
        public int Data; // 데이터

        // 연결할 오브젝트, 저장할 데이터 가져옴
        public Node(GameObject obj, int Num)
        {
            nodeBullet = obj;
            Next = null;
            Data = Num;
            obj.SetActive(false);
        }
    }

    // 실행 시 BulletLinkedList 생성
    void Start()
    {
        LinkedList = new BulletLinkedList(BulletPrefab, 10); // 10개

        // 이건 노드 연결 여부를 확인할 디버그용
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
