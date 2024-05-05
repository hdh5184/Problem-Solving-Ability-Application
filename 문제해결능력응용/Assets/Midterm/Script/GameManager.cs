using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public static Vector3 playerPos;
    public static bool isDoorLock = true;

    private void Update()
    {
        playerPos = player.transform.position;
    }
}
