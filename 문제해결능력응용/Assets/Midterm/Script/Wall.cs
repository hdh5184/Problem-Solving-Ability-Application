using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Wall : MonoBehaviour
{
    public Vector3 wallPos;
    public Vector3 wallScale;

    public Vector3 doorPos;
    public Vector3 doorHinge;
    public bool isDoor;

    float firstDoorDegY = 0f;
    float doorDegY = 0f;
    bool isDoorOpen = false;
    bool isDoorOpenLeft = false;

    void Start()
    {
        transform.position = wallPos;
        transform.localScale = wallScale;
        doorPos = wallPos;
        firstDoorDegY = transform.eulerAngles.y;

        if (isDoor)
        {
            if (transform.eulerAngles.y == 0)
                doorHinge = new Vector3(wallPos.x, wallPos.y, wallPos.z + 1f);
            else doorHinge = new Vector3(wallPos.x + 1f, wallPos.y, wallPos.z);
        }
    }

    private void Update()
    {
        if (isDoor)
        {
            if (GameManager.isDoorLock)
            {
                GetComponent<BoxCollider>().isTrigger = false; return;
            }
            else
                GetComponent<BoxCollider>().isTrigger = true;

            Collider[] hit = Physics.OverlapBox(doorPos, Vector3.one, Quaternion.identity);
            
            bool isHit = false;

            foreach (var item in hit)
            {
                if (item.tag == "Player")
                {
                    isHit = true;

                    if (isDoorOpen == true) break;
                    else
                    {
                        isDoorOpen = true;
                        Vector3 dis = doorPos - GameManager.playerPos;
                        dis = Quaternion.AngleAxis(-firstDoorDegY, Vector3.up) * dis;
                        Debug.Log(dis);

                        isDoorOpenLeft = (dis.x < 0) ? true : false;
                    }
                    break;
                }
            }

            float deg = 180 * Time.deltaTime;

            if (isHit)
            {
                switch (isDoorOpenLeft)
                {
                    case true:
                        if (doorDegY < 90)
                        {
                            doorDegY += deg;
                            transform.RotateAround(doorHinge, Vector3.up, deg);
                        }
                        break;
                    case false:
                        if (doorDegY > -90)
                        {
                            doorDegY -= deg;
                            transform.RotateAround(doorHinge, Vector3.up, -deg);
                        }  
                        break;
                }
            }
            else
            {
                isDoorOpen = false;
                if (doorDegY != 0)
                {
                    switch (isDoorOpenLeft)
                    {
                        case true:
                            deg = (doorDegY < deg) ? doorDegY : deg;
                            doorDegY -= deg;
                            transform.RotateAround(doorHinge, Vector3.up, -deg);
                            break;
                        case false:
                            deg = (-doorDegY < deg) ? -doorDegY : deg;
                            doorDegY += deg;
                            transform.RotateAround(doorHinge, Vector3.up, deg);
                            break;
                    }
                }
            }

        }
    }

    void OnDrawGizmos()
    {
        if (isDoor) Gizmos.DrawCube(doorPos, Vector3.one * 2);
    }

}
