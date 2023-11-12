using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool IsWalkable {get; set;}
    public Vector3 WorldPosition {get; set;}

    public Node(bool isWalkable, Vector3 worldPosition){
        IsWalkable = isWalkable;
        WorldPosition = worldPosition;
    }
}
