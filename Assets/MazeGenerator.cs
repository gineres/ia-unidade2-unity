using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private MazeCell mazeCellPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject healPrefab;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private int mazeWidth;
    [SerializeField] private int mazeDepth;
    [SerializeField] private float wallSpacing = 1.5f;
    [SerializeField] private int enemiesQuantity = 1;
    [SerializeField] private int healQuantity = 1;
    [SerializeField] private int bombQuantity = 1;

    private MazeCell[,] mazeGrid;

    void Start()
    {
        mazeGrid = new MazeCell[mazeWidth, mazeDepth];

        float centerX = ((mazeWidth - 1) * wallSpacing) / -2f;
        float centerY = ((mazeDepth - 1) * wallSpacing) / -2f;

        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeDepth; y++)
            {
                float xPos = x * wallSpacing + centerX;
                float yPos = y * wallSpacing + centerY;

                mazeGrid[x, y] = Instantiate(mazeCellPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
            }
        }

        GenerateMaze(null, mazeGrid[0, 0]);

        Grid grid = GameObject.Find("A*").GetComponent<Grid>();
        grid.CreateGrid();
        grid.PlaceElements(enemyPrefab, enemiesQuantity);
        grid.PlaceElements(healPrefab, healQuantity);
        grid.PlaceElements(bombPrefab, bombQuantity);
    }

    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        MazeCell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);

        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = Mathf.FloorToInt((currentCell.transform.position.x + (mazeWidth * wallSpacing / 2)) / wallSpacing);
        int y = Mathf.FloorToInt((currentCell.transform.position.y + (mazeDepth * wallSpacing / 2)) / wallSpacing);

        if (x + 1 < mazeWidth)
        {
            var cellToRight = mazeGrid[x + 1, y];

            if (cellToRight.IsVisited == false)
            {
                yield return cellToRight;
            }
        }

        if (x - 1 >= 0)
        {
            var cellToLeft = mazeGrid[x - 1, y];

            if (cellToLeft.IsVisited == false)
            {
                yield return cellToLeft;
            }
        }

        if (y + 1 < mazeDepth)
        {
            var cellToFront = mazeGrid[x, y + 1];

            if (cellToFront.IsVisited == false)
            {
                yield return cellToFront;
            }
        }

        if (y - 1 >= 0)
        {
            var cellToBack = mazeGrid[x, y - 1];

            if (cellToBack.IsVisited == false)
            {
                yield return cellToBack;
            }
        }
    }


    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null)
        {
            return;
        }

        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }

        if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        if (previousCell.transform.position.y < currentCell.transform.position.y)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }

        if (previousCell.transform.position.y > currentCell.transform.position.y)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }
}
