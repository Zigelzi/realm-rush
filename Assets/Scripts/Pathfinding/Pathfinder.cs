using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    public Vector2Int StartCoordinates { get { return startCoordinates; } }

    [SerializeField] Vector2Int destinationCoordinates;
    public Vector2Int DestinationCoordinates { get { return destinationCoordinates; } }

    Node startNode;
    Node destinationNode;
    Node currentSearchNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    
    // Start is called before the first frame update
    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.Grid;
            startNode = grid[startCoordinates];
            startNode.isStart = true;

            destinationNode = grid[destinationCoordinates];
            destinationNode.isDestination = true;

            
        }
        
    }

    void Start()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(startCoordinates);
    }

    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(coordinates);
        return BuildPath();
    }

    void BreadthFirstSearch(Vector2Int coordinates)
    {
        // Clear the previous path
        frontier.Clear();
        reached.Clear();

        startNode.isWalkable = true;
        destinationNode.isWalkable = true;

        bool isSearching = true;

        // Add the starting node as the first item of the queue
        frontier.Enqueue(grid[coordinates]);
        // At the beginning we've already reached the starting node, because it's... starting node. Duh.
        reached.Add(coordinates, grid[coordinates]);

        // Then we start exploring the surrounding nodes as long as there's new nodes to be discovered
        while (frontier.Count > 0 && isSearching)
        {
            // Get the first node from the queue as the node which neighbors we search for
            currentSearchNode = frontier.Dequeue();
            // Mark it as explored
            currentSearchNode.isExplored = true;

            ExploreNeighbors();

            // If we find the destination, stop searching
            if (currentSearchNode.coordinates == destinationNode.coordinates)
            {
                isSearching = false;
            }
        }
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int searchCoordinates = currentSearchNode.coordinates + direction;
            if (grid.ContainsKey(searchCoordinates))
            {
                Node foundNode = grid[searchCoordinates];
                if (foundNode.isWalkable)
                {
                    neighbors.Add(foundNode);
                }
                
            }

            foreach (Node neighbor in neighbors)
            {
                if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
                {
                    neighbor.connectedTo = currentSearchNode;
                    // Mark the node as being reached and then add it to the queue to be explored
                    reached.Add(neighbor.coordinates, neighbor);
                    frontier.Enqueue(neighbor);
                }
            }
        }
    }

    List<Node> BuildPath()
    {
        // Build path backwards from destination node via nodes connected to it
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }
        
        path.Reverse();
        return path;

    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;

            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = previousState;

            if (newPath.Count <= 1)
            {
                GetNewPath();
                Debug.Log("Placing tower in here is not allowed. It would block the path");
                return true;
            }
        }

        return false;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }

}
