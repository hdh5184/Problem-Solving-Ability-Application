using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Palmmedia.ReportGenerator.Core;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[CustomEditor(typeof(EnemyRangeEditor))]
public class EnemyRangeEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Enemy enemy = (Enemy)target;
    }
}
#endif

public class Enemy : MonoBehaviour
{
    public float rangeX = 5;
    public float rangeZ = 5;

    float moveRangeMinX, moveRangeMaxX, moveRangeMinZ, moveRangeMaxZ;
    float angle;
    Vector3 rangePointVec;

    void Start()
    {
        angle = Random.Range(-180f, 180f);
        //moveVec = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
        transform.rotation = Quaternion.Euler(0, angle, 0);
        moveRangeMinX = transform.position.x - rangeX;
        moveRangeMaxX = transform.position.x + rangeX;
        moveRangeMinZ = transform.position.z - rangeZ;
        moveRangeMaxZ = transform.position.z + rangeZ;
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
        Gizmos.color = Color.yellow;
        if (rangePointVec != null)
            Gizmos.DrawWireCube(rangePointVec, new Vector3(rangeX * 2, 1, rangeZ * 2));
        else
            Gizmos.DrawWireCube(transform.position, new Vector3(rangeX * 2, 1, rangeZ * 2));
    }
#endif
}
