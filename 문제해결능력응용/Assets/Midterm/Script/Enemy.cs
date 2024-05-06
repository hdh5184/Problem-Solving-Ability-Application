using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float range = 5;

    Rigidbody rb;
    Camera camera;

    GameObject player;

    Stack<float> MoveHistory;

    float angle;
    float fieldTime;
    Vector3 rangePointVec = Vector3.zero;

    MoveType moveType;
    enum MoveType {Idle, Detect, Search, Back}

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = GetComponent<Camera>();
        moveType = MoveType.Idle;
        player = GameManager.gm.player;

        fieldTime = 0f;
        MoveHistory = new Stack<float>();
        rangePointVec = transform.position;
        angle = Random.Range(-180f, 180f);
        transform.rotation = Quaternion.Euler(0, angle, 0);
        rangePointVec = transform.position;
    }

    
    void Update()
    {
        
        switch (moveType)
        {
            case MoveType.Idle:
                fieldTime += Time.deltaTime;
                if (fieldTime > 2f)
                {
                    angle = 0;
                    fieldTime = 0;
                    rb.velocity = Vector3.zero;
                    angle = Random.Range(-180f, 180f);
                    transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y + angle, 0);
                }

                transform.Translate(Vector3.forward * 1.5f * Time.deltaTime);
                Vector3 distanceVec = rangePointVec - transform.position;

                if (distanceVec.magnitude > range)
                {
                    transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y + 180, 0);
                    transform.Translate(Vector3.forward * 4f * Time.deltaTime);
                }
                break;
            case MoveType.Detect:
                transform.LookAt(player.transform.position, Vector3.up);
                transform.Translate(Vector3.forward * 3f * Time.deltaTime);
                MoveHistory.Push(transform.localEulerAngles.y);
                break;
            case MoveType.Search:
                fieldTime += Time.deltaTime;

                if (fieldTime < 3f)
                    transform.Rotate(Vector3.up * 30 * Time.deltaTime);
                else if (fieldTime < 6f)
                    transform.Rotate(Vector3.up * -60 * Time.deltaTime);
                else
                {
                    fieldTime = 0;
                    moveType = MoveType.Back;
                }
                break;
            case MoveType.Back:
                transform.rotation = Quaternion.Euler(new Vector3(
                    0, MoveHistory.Pop() + 180, 0));
                transform.Translate(Vector3.forward * 2f * Time.deltaTime);
                if (MoveHistory.Count == 0) moveType = MoveType.Idle;
                break;
        }

        Vector3 playerPos = player.transform.position;
        float sphereRadius = player.transform.localScale.x * 0.5f;

        if (IsSphereInsideFrustum(camera, playerPos, sphereRadius))
        {
            moveType = MoveType.Detect;
            fieldTime = 0;
        }
        else
        {
            if (moveType == MoveType.Detect) moveType = MoveType.Search;
        }
    }

    private bool IsSphereInsideFrustum(Camera camera, Vector3 playerPos, float sphereRadius)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        foreach (var plane in planes)
        {
            float distanceToPlane = plane.GetDistanceToPoint(playerPos);

            if (distanceToPlane < -sphereRadius)
                return false;
            else if (Mathf.Abs(distanceToPlane) < sphereRadius)
            {
                Vector3 planeNormal = transform.forward.normalized;
                Vector3 pointOnPlane = transform.position;

                Plane enemyPlane = new Plane(planeNormal, pointOnPlane);
                float distanceToPlane2 = enemyPlane.GetDistanceToPoint(playerPos);

                if (!enemyPlane.GetSide(player.transform.position)) return false;
                if (distanceToPlane2 - sphereRadius >= GetComponent<Camera>().farClipPlane)
                    return false;
                else return true;
            }
        }
        return true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (rangePointVec == Vector3.zero)
            Gizmos.DrawWireSphere(transform.position, range);
        else
            Gizmos.DrawWireSphere(rangePointVec, range);

    }
}
