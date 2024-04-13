using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CustomEditor(typeof(StaticMeshGenerator))]
public class StaticMeshGeneratorEditor : Editor
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

public class StaticMeshGenerator : MonoBehaviour
{
    Vector3[] normalTemp;
    bool drawGizmos;

    public void GenerateMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[20];       // 정점 좌표
        int[] triangleIndiecs = new int[30 * 3];    // 삼각형 면
        Vector3[] normals = new Vector3[20];        // 삼각형 면의 법선 벡터

        normalTemp = null;
        drawGizmos = false;

        // 1. 정점 저장
        Vector3 pantagonVecA = new Vector3(0, 1, -1);
        Vector3 pantagonVecB = new Vector3(0, -(1 - 1 / 1.618f), -1);

        for (int i = 0; i < 5; i++)
            vertices[i] = Quaternion.Euler(0, 0, 72 * i) * pantagonVecA;
        for (int i = 5; i < 10; i++)
            vertices[i] = Quaternion.Euler(0, 0, 72 * i) * pantagonVecB;
        for (int i = 0; i < 10; i++)
            vertices[i + 10] = new Vector3(vertices[i].x, vertices[i].y, -vertices[i].z);


        // 2. 삼각형 면 저장
        for (int i = 0; i < 5; i++) // 정면 별
        {
            triangleIndiecs[i * 3] = i;
            triangleIndiecs[i * 3 + 1] = ((i + 1) % 5) + 5;
            triangleIndiecs[i * 3 + 2] = (i + 2) % 5;
        }
        for (int i = 0; i < 5; i++) // 후면 별
        {
            triangleIndiecs[(5 + i) * 3 + 2] = i + 10;
            triangleIndiecs[(5 + i) * 3 + 1] = (((i + 1) % 5) + 5) + 10;
            triangleIndiecs[(5 + i) * 3] = ((i + 2) % 5) + 10;
        }
        for (int i = 0; i < 5; i++) // 기둥 A1
        {
            triangleIndiecs[(10 + i) * 3] = i;
            triangleIndiecs[(10 + i) * 3 + 1] = i + 10;
            triangleIndiecs[(10 + i) * 3 + 2] = ((i + 2) % 5) + 15;
        }
        for (int i = 0; i < 5; i++) // 기둥 B1
        {
            triangleIndiecs[(15 + i) * 3] = i + 5;
            triangleIndiecs[(15 + i) * 3 + 1] = i + 15;
            triangleIndiecs[(15 + i) * 3 + 2] = ((i + 2) % 5) + 10;
        }
        for (int i = 0; i < 5; i++) // 기둥 A2
        {
            triangleIndiecs[(20 + i) * 3] = ((i + 2) % 5) + 15;
            triangleIndiecs[(20 + i) * 3 + 1] = ((i + 2) % 5) + 5;
            triangleIndiecs[(20 + i) * 3 + 2] = i;
        }
        for (int i = 0; i < 5; i++) // 기둥 B2
        {
            triangleIndiecs[(25 + i) * 3] = ((i + 2) % 5) + 10;
            triangleIndiecs[(25 + i) * 3 + 1] = ((i + 2) % 5);
            triangleIndiecs[(25 + i) * 3 + 2] = i + 5;
        }

        // 3. 삼각형 면의 법선 벡터 저장
        for (int i = 0; i < 20; i++)
        {
            normals[i] = Vector3.Cross(
                vertices[triangleIndiecs[i * 3 + 1]] - vertices[triangleIndiecs[i * 3]],
                vertices[triangleIndiecs[i * 3 + 2]] - vertices[triangleIndiecs[i * 3]])
                .normalized;
        }

        // 4. 메쉬 데이터 저장
        mesh.vertices = vertices;
        mesh.triangles = triangleIndiecs;
        mesh.normals = normals;


        // @. MeshFilter 재설정
        if (this.GetComponent<MeshFilter>() != null)
        {
            MeshFilter preMeshFilter = this.GetComponent<MeshFilter>();
            DestroyImmediate(preMeshFilter);
        }

        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();


        // A. 메쉬 출력
        mf.mesh = mesh;


        // *. 디버그 (Gizmos 출력, 정점 좌표 출력)
        // normalTemp = normals; drawGizmos = true;
        // foreach (var item in vertices) Debug.Log(item);
    }

    
    private void OnDrawGizmos()
    {
        if (!drawGizmos) return;

        Gizmos.color = Color.red;
        foreach (var item in normalTemp)
        {
            Gizmos.DrawLine(item, item * 2);
        }
        
    }
}

    