using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private MazeCell mazeCell;
    [SerializeField] private int mazeWidth;
    [SerializeField] private int mazeHeight;

    private MazeCell[,] mazeMap; 

    // Start is called before the first frame update
    void Start()
    {
        mazeMap = new MazeCell[mazeWidth, mazeHeight];

        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                mazeMap[x,y] = Instantiate(mazeCell, new Vector2(x + ((mazeWidth - 1)/-2f),y + ((mazeHeight - 1)/-2f)), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
