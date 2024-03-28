using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBullet : MonoBehaviour
{
    // 충돌 시 새로운 Node에 담을 Data 저장
    public Data data;

    void Update()
    {
        transform.Translate(Vector2.right * 5f * Time.deltaTime);
    }
}
