using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool IsWalkable {get; set;}
    public Vector3 WorldPosition {get; set;}

    public int gCost;
    public int hCost;

    public int gridX;
    public int gridY;
    public Node parent;

    public Node(bool isWalkable, Vector3 worldPosition, int gridX, int gridY){
        IsWalkable = isWalkable;
        WorldPosition = worldPosition;
        this.gridX = gridX;
        this.gridY = gridY;
    }

    public int fCost {
        get {
            return gCost + hCost;
        }
    }
}
