using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] nodeGrid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    public GameObject visualization;

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

        if (nodeGrid != null)
        {
            foreach (Node n in nodeGrid)
            {
                Gizmos.color = (n.IsWalkable) ? Color.white:Color.red;
                Gizmos.DrawWireCube(n.WorldPosition, Vector3.one * (nodeDiameter-.1f));
            }
        }
    }

    void Start()
    {
        nodeDiameter = nodeRadius*2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter); // Calculando o tamanho da malha com base no nivel de detalhe pedido (nodeRadius)
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);
    }

    public void CreateGrid(){
        nodeGrid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.up * gridWorldSize.y/2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableMask));
                nodeGrid[x,y] = new Node(walkable, worldPoint);
            }
        }
    }
}
