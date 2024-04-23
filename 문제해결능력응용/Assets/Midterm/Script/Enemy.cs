using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float moveRangeMinX, moveRangeMaxX, moveRangeMinZ, moveRangeMaxZ;
    float angle;
    Vector3 rangePointVec;

    void Start()
    {
        angle = Random.Range(-180f, 180f);
        //moveVec = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
        transform.rotation = Quaternion.Euler(0, angle, 0);
        moveRangeMinX = transform.position.x - 5;
        moveRangeMaxX = transform.position.x + 5;
        moveRangeMinZ = transform.position.z - 5;
        moveRangeMaxZ = transform.position.z + 5;
        rangePointVec = transform.position;
    }

    
    void Update()
    {
        transform.Translate(Vector3.forward * 1.5f * Time.deltaTime);
        if (transform.position.x < moveRangeMinX || transform.position.x > moveRangeMaxX ||
            transform.position.z < moveRangeMinZ || transform.position.z > moveRangeMaxZ)
        {
            if (angle >= 0) angle += Mathf.PI;
            if (angle < 0) angle -= Mathf.PI;
            transform.rotation = Quaternion.Euler(0, angle, 0);

        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(rangePointVec, new Vector3(10, 1, 10));
    }
#endif
}
