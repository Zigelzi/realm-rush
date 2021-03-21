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
    Vector2Int coordinates = new Vector2Int();
    public bool IsPlaceable { get { return isPlaceable; } }
    public bool HasTower { get { return hasTower; } set { hasTower = value; } }

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    void Start()
    {
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
        if (isPlaceable && !HasBuilding())
        {
            bool towerBuilt = towerPrefab.Build(towerPrefab, this);
            if (towerBuilt)
            {
                hasTower = true;
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
