using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTreeNode
{
    public QTreeNode currentNode;
    public QTreeNode[] leafNode;

    public GameObject obj;

    public float minX, minY, maxX, maxY;
    public float posX, posY;
    public int level;

    public QTreeNode(float minX, float minY, float maxX, float maxY, int level)
    {
        this.minX = minX; this.minY = minY;
        this.maxX = maxX; this.maxY = maxY;
        this.level = level;

        posX = (minX + maxX) / 2;
        posY = (minY + maxY) / 2;
    }
}
