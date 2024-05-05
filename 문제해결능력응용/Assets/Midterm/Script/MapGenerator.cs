using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject Terrain_Plane;
    public GameObject wallPrefab;
    public GameObject doorPrefab;
    public GameObject keyPrefab;
    public GameObject playerPrefab;

    public GameObject playerStartPos;
    public GameObject playerGoalPos;

    [SerializeField] private int mapX, mapY;
    [SerializeField] private float PlaneScale;
    public int[][] Wall;


    void Awake()
    {
        GameObject walls = new GameObject("Walls");
        PlaneScale = Terrain_Plane.transform.localScale.x;

        TextAsset data = Resources.Load("MapGenerator") as TextAsset;
        string[] lines = data.text.Split('\n');
        Wall = new int[lines.Length][];

        for (int i = 0; i < lines.Length; i++)
        {
            string[] wallNum = lines[i].Split(',');
            int[] tempNum = new int[wallNum.Length];

            for (int j = 0; j < wallNum.Length; j++)
            {
                tempNum[j] = int.Parse(wallNum[j]);
            }
            Wall[i] = tempNum;
            //Debug.Log(Wall[i][1]);
        }
        mapX = Wall.Length; mapY = Wall[0].Length;

        float wallScale = PlaneScale * 10 / Wall.Length;
        float wallPosMin = transform.position.x - (PlaneScale * 5);

        for (int i = 0; i < Wall.Length; i++)
        {
            for (int j = 0; j < Wall[i].Length; j++)
            {
                GameObject obj = null;

                switch (Wall[i][j])
                {
                    case 1:
                    case 2:
                        obj = Instantiate(wallPrefab, walls.transform);
                        obj.GetComponent<Wall>().wallScale = new Vector3(wallScale, Wall[i][j], wallScale);
                        obj.GetComponent<Wall>().isDoor = false;
                        break;
                    case 4:
                        obj = Instantiate(keyPrefab, walls.transform);
                        obj.GetComponent<Wall>().wallScale = new Vector3(wallScale, Wall[i][j], wallScale);
                        obj.GetComponent<Wall>().isDoor = false;
                        GameManager.gm.key = obj;
                        break;
                    case 3:
                        obj = Instantiate(doorPrefab, walls.transform);
                        obj.GetComponent<Wall>().wallScale = new Vector3(wallScale * 0.1f, 2, wallScale);
                        obj.GetComponent<Wall>().isDoor = true;
                        if (Wall[i][j - 1] == 0)
                            obj.transform.rotation = Quaternion.Euler(0, 90, 0);
                        break;

                }

                if (obj != null)
                {
                    obj.GetComponent<Wall>().wallPos = new Vector3(
                    wallPosMin + wallScale * (i + 0.5f),
                    0.5f * obj.transform.localScale.y,
                    wallPosMin + wallScale * (j + 0.5f));
                }
            }
        }

        playerPrefab.GetComponent<PlayerController>().startPos = new Vector3(
            playerStartPos.transform.position.x, 1f, playerStartPos.transform.position.z);
    }
}
