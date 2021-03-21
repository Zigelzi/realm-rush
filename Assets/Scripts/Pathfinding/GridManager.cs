using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    void Awake()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                AddNodeToGrid(x, y);
            }
        }
    }

    void AddNodeToGrid(int x, int y)
    {
        Vector2Int coordinates = new Vector2Int(x, y);
        Node node = new Node(coordinates, true);
        grid.Add(coordinates, node);
        //Debug.Log($"Node coordinates: {grid[coordinates].coordinates} and it's walkable: {grid[coordinates].isWalkable}");
    }

    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }

        return null;
    }
}
