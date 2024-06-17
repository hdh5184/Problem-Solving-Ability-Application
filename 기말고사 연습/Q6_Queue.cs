using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Q6_Queue : MonoBehaviour
{
    // 1. 자료구조, 기타 변수
    Stack<int> preQueue, outQueue;
    int queueNum, queueCount;

    // 2. 텍스트
    public TextMeshProUGUI Text_ShowQueueList;
    public TextMeshProUGUI Text_ShowDequeueElement;

    private void OnEnable()
    {
        preQueue = new Stack<int>();
        outQueue = new Stack<int>();
        queueNum = 0;
        queueCount = 0;

        Text_ShowQueueList.text = "QueueList : \n";
        Text_ShowDequeueElement.text = "CurrentDequeueElement : ";
    }

    public void Enqueue()
    {
        queueNum++;

        if (queueCount == 0)
        {
            outQueue.Push(queueNum);
            queueCount++;
        }
        else
        {
            for (int i = 0; i < queueCount; i++)
            {
                preQueue.Push(outQueue.Pop());
            }

            preQueue.Push(queueNum);
            queueCount++;

            for (int i = 0; i < queueCount; i++)
            {
                outQueue.Push(preQueue.Pop());
            }
        }

        ShowQueue();
    }

    public void Dequeue()
    {
        if (queueCount == 0)
        {
            Debug.Log("Queue가 비어있습니다.");
        }
        else
        {
            int DequeueElement = outQueue.Pop();
            Text_ShowDequeueElement.text =
                $"CurrentDequeueElement : " +
                $"<size=72>{DequeueElement.ToString()}</size>";
            queueCount--;
        }

        ShowQueue();
    }

    void ShowQueue()
    {
        Text_ShowQueueList.text = "QueueList : \n";

        foreach (var item in outQueue)
        {
            Text_ShowQueueList.text += $"{item.ToString()}, ";
        }
    }
}
