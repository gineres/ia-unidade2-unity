using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject upWall;
    [SerializeField] private GameObject downWall;
    [SerializeField] private GameObject middleWall;

    public bool IsVisited {get; private set;}

    public void Visit(){
        IsVisited = true;
        middleWall.SetActive(false);
    }

    public void ClearLeftWall(){
        leftWall.SetActive(false);
    }

    public void ClearRightWall(){
        rightWall.SetActive(false);
    }

    public void ClearUpWall(){
        upWall.SetActive(false);
    }

    public void ClearDownWall(){
        downWall.SetActive(false);
    }
}
