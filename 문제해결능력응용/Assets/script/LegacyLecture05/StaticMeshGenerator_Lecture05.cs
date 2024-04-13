using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StaticMeshGenerator))]
public class StaticMeshGeneratorEditor_Lecture05 : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        StaticMeshGenerator script = (StaticMeshGenerator)target;

        if (GUILayout.Button("Generate Mesh"))
        {
            script.GenerateMesh();
        }
    }
}

public class StaticMeshGenerator_Lecture05 : MonoBehaviour
{
    public void GenerateMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[]
        {
            new Vector3 (0.00f, 0.00f, 1.00f),
            new Vector3 (0.00f, 1.00f, 1.00f),
            new Vector3 (0.225f, 0.30f, 1.00f),
            new Vector3 (0.95f, 0.30f, 1.00f),
            new Vector3 (0.36f, -0.12f, 1.00f),
            new Vector3 (0.58f, -0.80f, 1.00f),
            new Vector3 (0.00f, -0.38f, 1.00f),
            new Vector3 (-0.58f, -0.80f, 1.00f),
            new Vector3 (-0.36f, -0.12f, 1.00f),
            new Vector3 (-0.95f, 0.30f, 1.00f),
            new Vector3 (-0.225f, 0.30f, 1.00f),

            new Vector3 (0.00f, 0.00f, -1.00f),
            new Vector3 (0.00f, 1.00f, -1.00f),
            new Vector3 (0.225f, 0.30f, -1.00f),
            new Vector3 (0.95f, 0.30f, -1.00f),
            new Vector3 (0.36f, -0.12f, -1.00f),
            new Vector3 (0.58f, -0.80f, -1.00f),
            new Vector3 (0.00f, -0.38f, -1.00f),
            new Vector3 (-0.58f, -0.80f, -1.00f),
            new Vector3 (-0.36f, -0.12f, -1.00f),
            new Vector3 (-0.95f, 0.30f, -1.00f),
            new Vector3 (-0.225f, 0.30f, -1.00f)
        };

        Vector3[] normals = new Vector3[]
        {
            new Vector3 (0.00f, 0.00f, 1.00f),
            new Vector3 (0.00f, 0.00f, 1.00f),
            new Vector3 (0.00f, 0.00f, 1.00f),
            new Vector3 (0.00f, 0.00f, 1.00f),
            new Vector3 (0.00f, 0.00f, 1.00f),
            new Vector3 (0.00f, 0.00f, 1.00f),
            new Vector3 (0.00f, 0.00f, 1.00f),
            new Vector3 (0.00f, 0.00f, 1.00f),
            new Vector3 (0.00f, 0.00f, 1.00f),
            new Vector3 (0.00f, 0.00f, 1.00f),
            new Vector3 (0.00f, 0.00f, 1.00f),

            new Vector3 (0.00f, 0.00f, -1.00f),
            new Vector3 (0.00f, 0.00f, -1.00f),
            new Vector3 (0.00f, 0.00f, -1.00f),
            new Vector3 (0.00f, 0.00f, -1.00f),
            new Vector3 (0.00f, 0.00f, -1.00f),
            new Vector3 (0.00f, 0.00f, -1.00f),
            new Vector3 (0.00f, 0.00f, -1.00f),
            new Vector3 (0.00f, 0.00f, -1.00f),
            new Vector3 (0.00f, 0.00f, -1.00f),
            new Vector3 (0.00f, 0.00f, -1.00f),
            new Vector3 (0.00f, 0.00f, -1.00f)
        };

        mesh.vertices = vertices;

        int[] triangleIndiecs = new int[]
        {
            0, 2, 1,    0, 3, 2,    0, 4, 3,    0, 5, 4,
            0, 6, 5,    0, 7, 6,    0, 8, 7,    0, 9, 8,
            0, 10, 9,   0, 1, 10,

            11, 12, 13,     11, 13, 14,     11, 14, 15,     11, 15, 16,
            11, 16, 17,     11, 17, 18,     11, 18, 19,     11, 19, 20,
            11, 20, 21,     11, 21, 12,

            1, 2, 12,   2, 13, 12,  2, 3, 13,   3, 14, 13,
            3, 4, 14,   4, 15, 14,  4, 5, 15,   5, 16, 15,
            5, 6, 16,   6, 17, 16,  6, 7, 17,   7, 18, 17,
            7, 8, 18,   8, 19, 18,  8, 9, 19,   9, 20, 19,
            9, 10, 20,   10, 21, 20,  10, 1, 21,   1, 12, 21,
        };

        mesh.triangles = triangleIndiecs;
        mesh.normals = normals;

        if (this.GetComponent<MeshFilter>() != null)
        {
            MeshFilter preMeshFilter = this.GetComponent<MeshFilter>();
            DestroyImmediate(preMeshFilter);
        }


        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();


        mf.mesh = mesh;
    }

    void Update()
    {
        
    }

    /*
    private void OnDrawGizmos()
    {
        Vector3[] vertices = new Vector3[]
        {
            new Vector3 (0.00f, 0.00f, 1.00f),
            new Vector3 (0.00f, 1.00f, 1.00f),
            new Vector3 (0.225f, 0.30f, 1.00f),
            new Vector3 (0.95f, 0.30f, 1.00f),
            new Vector3 (0.36f, -0.12f, 1.00f),
            new Vector3 (0.58f, -0.80f, 1.00f),
            new Vector3 (0.00f, -0.38f, 1.00f),
            new Vector3 (-0.58f, -0.80f, 1.00f),
            new Vector3 (-0.36f, -0.12f, 1.00f),
            new Vector3 (-0.95f, 0.30f, 1.00f),
            new Vector3 (-0.225f, 0.30f, 1.00f),

            new Vector3 (0.00f, 0.00f, -1.00f),
            new Vector3 (0.00f, 1.00f, -1.00f),
            new Vector3 (0.225f, 0.30f, -1.00f),
            new Vector3 (0.95f, 0.30f, -1.00f),
            new Vector3 (0.36f, -0.12f, -1.00f),
            new Vector3 (0.58f, -0.80f, -1.00f),
            new Vector3 (0.00f, -0.38f, -1.00f),
            new Vector3 (-0.58f, -0.80f, -1.00f),
            new Vector3 (-0.36f, -0.12f, -1.00f),
            new Vector3 (-0.95f, 0.30f, -1.00f),
            new Vector3 (-0.225f, 0.30f, -1.00f)
        };

        int[] triangleIndiecs = new int[]
        {
            0, 2, 1,    0, 3, 2,    0, 4, 3,    0, 5, 4,
            0, 6, 5,    0, 7, 6,    0, 8, 7,    0, 9, 8,
            0, 10, 9,   0, 1, 10,

            11, 12, 13,     11, 13, 14,     11, 14, 15,     11, 15, 16,
            11, 16, 17,     11, 17, 18,     11, 18, 19,     11, 19, 20,
            11, 20, 21,     11, 21, 12,

            1, 2, 12,   2, 13, 12,  2, 3, 13,   3, 14, 13,
            3, 4, 14,   4, 15, 14,  4, 5, 15,   5, 16, 15,
            5, 6, 16,   6, 17, 16,  6, 7, 17,   7, 18, 17,
            7, 8, 18,   8, 19, 18,  8, 9, 19,   9, 20, 19,
            9, 10, 20,   10, 21, 20,  10, 1, 21,   1, 12, 21,
        };

        Gizmos.color = Color.red;

        for (int i = 0; i < triangleIndiecs.Length; i += 3)
        {
            Vector3 a = vertices[triangleIndiecs[i]];
            Vector3 b = vertices[triangleIndiecs[i+1]];
            Vector3 c = vertices[triangleIndiecs[i+2]];

            Vector3 side1 = b - a;
            Vector3 side2 = c - a;

            Vector3 perp = Vector3.Cross(side1, side2).normalized;

            //var perpLength = perp.magnitude;
            //perp /= perpLength;

            Gizmos.DrawLine(perp, perp * 2);
        }

    }
    */
}

    