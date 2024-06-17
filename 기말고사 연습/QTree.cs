using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTree : MonoBehaviour
{
    public float width = 10;
    public QTree tree;

    List<QTreeNode> drawNodeList = new List<QTreeNode>();

    public QTreeNode Root;

    float treeWidth;

    public GameObject prefab;
    public Camera thisCamera;

    /*
    public QTree(float width)
    {
        
        Debug.Log(treeWidth);
    }
    */

    public void Start()
    {
        Root = null;
        treeWidth = width;
        Debug.Log(treeWidth);

        drawNodeList = new List<QTreeNode>();

        thisCamera = thisCamera.GetComponent<Camera>();
    }

    public void InsertNode(GameObject getObj)
    {
        if (Root == null)
        {
            Root = new QTreeNode
                (-treeWidth, -treeWidth, treeWidth, treeWidth, 0);
            Root.obj = getObj;

            Debug.Log(treeWidth);
            Debug.Log($"{Root.minX}, {Root.minY}, {Root.maxX}, {Root.maxY}, {Root.level}");
            return;
        }

        // else
        QTreeNode node = Root;

        while (true)
        {
            if (node.obj == null && node.leafNode == null)
            {
                node.obj = getObj;
                Debug.Log($"{node.minX}, {node.minY}, {node.maxX}, {node.maxY}, {node.level}");
                Debug.Log(node.level);
                break;
            }

            // else
            if (node.leafNode == null)
            {
                node.leafNode = new QTreeNode[4];

                node.leafNode[0] = new QTreeNode(
                    node.minX, node.posY, node.posX, node.maxY, node.level + 1);
                node.leafNode[1] = new QTreeNode(
                    node.posX, node.posY, node.maxX, node.maxY, node.level + 1);
                node.leafNode[2] = new QTreeNode(
                    node.minX, node.minY, node.posX, node.posY, node.level + 1);
                node.leafNode[3] = new QTreeNode(
                    node.posX, node.minY, node.maxX, node.posY, node.level + 1);

                QTreeNode tempNode = setLeafNode(node, node.obj);
                Debug.Log(tempNode.level);
                tempNode.obj = node.obj;
                node.obj = null;
            }

            QTreeNode nextNode = setLeafNode(node, getObj);
            node = nextNode;
        }
    }

    public QTreeNode setLeafNode(QTreeNode node, GameObject obj)
    {
        if (obj.transform.position.x <= node.posX)
        {
            if (obj.transform.position.y >= node.posY)
                node = node.leafNode[0];
            else
                node = node.leafNode[2];
        }
        else
        {
            if (obj.transform.position.y >= node.posY)
                node = node.leafNode[1];
            else
                node = node.leafNode[3];
        }

        return node;
    }

    public void SearchNode(QTreeNode node)
    {
        drawNodeList.Add(node);

        try
        {
            if (node.leafNode == null) return;
        }
        catch (System.Exception ex)
        {
            return;
        }
        

        for (int i = 0; i < node.leafNode.Length; i++)
        {
            SearchNode(node.leafNode[i]);
        }
    }

    private void OnDrawGizmos()
    {
        drawNodeList = new List<QTreeNode>();

        Gizmos.color = Color.yellow;

        QTreeNode node = Root;
        if (Root == null) return;

        SearchNode(node);


        /*
        foreach (QTreeNode item in drawNodeList)
        {
            float posX = (item.minX + item.maxX) / 2;
            float posY = (item.minY + item.maxY) / 2;
            float drawWidth = Mathf.Abs(item.maxX - item.minX);

            Gizmos.DrawWireCube(
                new Vector2(posX, posY), new Vector3(drawWidth, drawWidth, 0));
        }
        */




        if (thisCamera == null)
        {
            Debug.LogError("카메라 컴포넌트를 찾을 수 없습니다.");
            return;
        }

        // 카메라의 프러스텀을 가져오기
        FrustumPlanes frustum = new FrustumPlanes(thisCamera);

        List<QTreeNode> cullingNodeList = new List<QTreeNode>();

        Queue<QTreeNode> cullingNode = new Queue<QTreeNode>();
        cullingNode.Enqueue(Root);

        do
        {
            QTreeNode getNode = cullingNode.Dequeue();

            if (getNode.leafNode != null)
            {
                foreach (QTreeNode item in getNode.leafNode)
                    cullingNode.Enqueue(item);
            }
            else
            {
                Vector3[] nodeBoundPoint = new Vector3[4];

                nodeBoundPoint[0] = new Vector3(getNode.minX, getNode.maxY);
                nodeBoundPoint[1] = new Vector3(getNode.maxX, getNode.maxY);
                nodeBoundPoint[2] = new Vector3(getNode.minX, getNode.minY);
                nodeBoundPoint[3] = new Vector3(getNode.maxX, getNode.minY);

                bool isInsidePoint = false;

                foreach (Vector3 point in nodeBoundPoint)
                {
                    if (frustum.IsInsideFrustum(point))
                        isInsidePoint = true;
                }

                if (isInsidePoint) cullingNodeList.Add(getNode);
            }

        } while (cullingNode.Count > 0);

        Gizmos.color = Color.red;

        foreach (QTreeNode item in cullingNodeList)
        {
            float posX = (item.minX + item.maxX) / 2;
            float posY = (item.minY + item.maxY) / 2;
            float drawWidth = Mathf.Abs(item.maxX - item.minX);

            Gizmos.DrawWireCube(
                new Vector2(posX, posY), new Vector3(drawWidth, drawWidth, 0));
        }
    }



    public void GenerateObj()
    {
        GameObject obj = Instantiate(prefab);

        float randomX = Random.Range(-width, width);
        float randomY = Random.Range(-width, width);

        obj.transform.position = new Vector2(randomX, randomY);
        InsertNode(obj);
    }






    private void Update()
    {
        if (Root == null) return;

        
    }
}

public class FrustumPlanes
{
    private readonly Plane[] planes;

    public FrustumPlanes(Camera camera)
    {
        planes = GeometryUtility.CalculateFrustumPlanes(camera);
    }

    public bool IsInsideFrustum(Vector3 point)
    {
        foreach (var plane in planes)
        {
            if (!plane.GetSide(point))
            {

                return false;
            }
        }
        return true;
    }
}
