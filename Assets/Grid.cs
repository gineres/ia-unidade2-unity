using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public Node[,] nodeGrid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;
    //public List<Node> path;

    void OnDrawGizmos()
    {/*
        if (nodeGrid != null)
        {
            if (path != null)
            {
                foreach (Node n in path){
                    Gizmos.color = Color.black;
                    Gizmos.DrawWireCube(n.WorldPosition, Vector3.one * (nodeDiameter-.1f));
                }
            }
        }*/
        
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

        if (nodeGrid != null)
        {
            foreach (Node n in nodeGrid)
            {
                Gizmos.color = (n.IsWalkable) ? Color.white:Color.red;
                /*
                if (path != null)
                {
                    if (path.Contains(n))
                    {
                        Gizmos.color = Color.black;
                    }
                }*/
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
        Debug.Log("criando grid");
        nodeGrid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.up * gridWorldSize.y/2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableMask));
                nodeGrid[x,y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public void PlaceElements(GameObject element, int elementQuantity){
        for (int x = 0; x < elementQuantity; x++)
        {
            Node enemyNode = nodeGrid[Random.Range(0, gridSizeX-1), Random.Range(0, gridSizeY-1)];
            if (enemyNode.IsWalkable)
            {
                Instantiate(element, enemyNode.WorldPosition, Quaternion.identity);
            }
            else
            {
                x--;
                continue;
            }
        }
    }

    public Node GetNodeFromWorldPoint(Vector3 worldPosition){
        float percentX = (worldPosition.x + gridWorldSize.x/2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y/2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX); // Evitar erros de tentar acessar coisas fora do limite do array
        percentY = Mathf.Clamp01(percentY);

        int x  = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y  = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return nodeGrid[x,y];
    }

    public List<Node> GetNeighbors(Node node){
        List<Node> neighbors = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 || y == 0) continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX <= gridSizeX && checkY >= 0 && checkY <= gridSizeY)
                {
                    neighbors.Add(nodeGrid[checkX,checkY]);
                }
            }
        }

        return neighbors;
    }
}
