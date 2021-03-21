using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int destinationCoordinates;

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
        }
        
    }

    private void Start()
    {
        if (gridManager != null)
        {
            startNode = grid[startCoordinates];
            startNode.isStart = true;

            destinationNode = grid[destinationCoordinates];
            destinationNode.isDestination = true;
        }
        
        BreadthFirstSearch();
        BuildPath();
    }

    void BreadthFirstSearch()
    {
        bool isSearching = true;
        // Add the starting node as the first item of the queue
        frontier.Enqueue(startNode);
        // Because we've reached our starting node we add it to the reached list
        reached.Add(startNode.coordinates, startNode);

        // Then we start finding the surrounding nodes as long as there's new nodes to be discovered
        while (frontier.Count > 0 && isSearching)
        {
            // Get the first node from the queue as the node which neighbors we search for
            currentSearchNode = frontier.Dequeue();
            // Mark it as explored
            currentSearchNode.isExplored = true;

            ExploreNeighbors();

            // If we find the destination, we don't need to search anymore
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

    

}
