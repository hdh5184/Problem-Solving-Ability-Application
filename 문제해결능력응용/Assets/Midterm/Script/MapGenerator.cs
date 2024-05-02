using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject Terrain_Plane;
    public GameObject wallPrefab;

    public float PlaneScale;
    public int[][] Wall;


    void Start()
    {
        Terrain_Plane.transform.localScale = new Vector3(PlaneScale, 1, PlaneScale);

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

        float wallScale = PlaneScale / 2;
        float wallPosMin = -PlaneScale / 2;

        for (int i = 0; i < Wall.Length; i++)
        {
            for (int j = 0; j < Wall[i].Length; j++)
            {
                if (Wall[i][j] == 0) continue;
                GameObject wallObj = Instantiate(wallPrefab);
                wallObj.GetComponent<Wall>().wallScale =
                    new Vector3(wallScale, 1, wallScale);
                wallObj.GetComponent<Wall>().wallPos =
                    new Vector3(wallPosMin + wallScale * i, 1, wallPosMin + wallScale * j);
            }
        }
    }

    void Update()
    {
        
    }
}
