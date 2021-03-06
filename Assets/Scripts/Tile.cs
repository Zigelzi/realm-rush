using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Tile : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    [SerializeField] bool hasTower = false;
    [SerializeField] Tower towerOnTile; // Tower on the tile
    [SerializeField] Tower towerPrefab;

    GridManager gridManager;
    Pathfinder pathFinder;
    Vector2Int coordinates = new Vector2Int();
    public bool IsPlaceable { get { return isPlaceable; } }
    public bool HasTower { get { return hasTower; } set { hasTower = value; } }

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<Pathfinder>();
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    void OnMouseDown()
    {
        Node node = gridManager.GetNode(coordinates);

        if (node == null)
        {
            Debug.Log("Trying to place tower outside the grid");
            return;
        }

        if (node.isWalkable && !HasBuilding())
        {
            bool buildingSuccessful = towerPrefab.Build(towerPrefab, this);
            if (buildingSuccessful)
            {
                hasTower = true;
                pathFinder.NotifyReceivers();
            }
        } else
        {
            Debug.Log("Not allowed to place tower here!");
        }
    }

    bool HasBuilding()
    {
        towerOnTile = GetComponentInChildren<Tower>();
        if (towerOnTile != null)
        {
            return true;
        } else
        {
            return false;
        }
    }

    
}
