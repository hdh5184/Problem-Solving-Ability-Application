using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Vector3 wallPos;
    public Vector3 wallScale;

    void Start()
    {
        transform.position = wallPos;
        transform.localScale = wallScale;
    }
}
