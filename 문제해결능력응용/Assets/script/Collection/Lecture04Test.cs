using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStrucuture;

// 총알 GameObject와 int 숫자가 저장된 구조체
public struct Data
{
    public GameObject bullet;
    public int Num;
}

public class Lecture04Test : MonoBehaviour
{
    public GameObject startPos;
    public GameObject prefab;

    public SelectTest selectTest;

    public enum SelectTest { Queue, Stack }

    DataStrucuture.Queue<Data> queue;
    DataStrucuture.Stack<Data> stack;

    // Component에서 selectTest 선택
    private void Awake()
    {
        switch (selectTest)
        {
            case SelectTest.Queue: queue = new DataStrucuture.Queue<Data>(); break;
            case SelectTest.Stack: stack = new DataStrucuture.Stack<Data>(); break;
        }
    }

    // Data 구조체와 함께 저장
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Data data = new Data();
            data.bullet = Instantiate(prefab);
            data.bullet.gameObject.SetActive(false);
            data.Num = i;

            switch (selectTest)
            {
                case SelectTest.Queue: queue.Enqueue(data); break;
                case SelectTest.Stack: stack.Push(data); break;
            }
        }
    }

    // 마우스 왼쪽 클릭으로 Queue 또는 Stack에서 빠진 Bullet의 Data 출력
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Data getData;

            switch (selectTest)
            {
                case SelectTest.Queue:
                    getData = queue.Dequeue();
                    Debug.Log($"Queue_Out : {getData.Num}");
                    break;
                case SelectTest.Stack:
                    getData = stack.Pop();
                    Debug.Log($"Stack_Out : {getData.Num}");
                    break;
                default: getData = new Data(); break;
            }
            
            getData.bullet.transform.position = startPos.transform.position;
            getData.bullet.gameObject.SetActive(true);
            getData.bullet.GetComponent<MovingBullet>().data = getData;
        }
    }

    // 충돌한 Bullet의 Data를 받아오며 Queue 또는 Stack에 입력
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Data getData = other.GetComponent<MovingBullet>().data;
            getData.bullet.gameObject.SetActive(false);

            switch (selectTest)
            {
                case SelectTest.Queue:
                    queue.Enqueue(getData);
                    Debug.Log($"Queue_In : {getData.Num}");
                    break;
                case SelectTest.Stack:
                    stack.Push(getData);
                    Debug.Log($"Stack_In : {getData.Num}");
                    break;
            }
        }
    }
}
