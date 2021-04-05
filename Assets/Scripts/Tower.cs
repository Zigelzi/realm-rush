using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] [Range(0, 50)] int buildCost = 25;

    void Start()
    {
        
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            Demolish();
        }
    }

    // Private methods
    bool IsBuildable(Tile tile)
    {
        if (tile.IsPlaceable && 
            !tile.HasTower)
        {
            return true;
        } else
        {
            return false;
        }
    }

    void Demolish()
    {
        Destroy(gameObject);
    }

    // Public methods

    public bool Build(Tower tower, Tile tile)
    {
        bool buildingSuccesful;
        Bank bank = FindObjectOfType<Bank>();
             

        if (bank == null)
        {
            Debug.Log("Bank not found");
            buildingSuccesful = false;
            return buildingSuccesful;
        }

        if (IsBuildable(tile) && bank.CanAffort(buildCost))
        {
            // Build the tower
            Instantiate(tower, tile.transform.position, tile.transform.rotation, tile.transform);
            bank.Withdraw(buildCost);

            BlockTile(tile);

            buildingSuccesful = true;
            return buildingSuccesful;
        }

        buildingSuccesful = false;
        return buildingSuccesful;
    }

    private void BlockTile(Tile tile)
    {
        GridManager gridmanager;
        gridmanager = FindObjectOfType<GridManager>();
        Vector2Int coordinates;

        coordinates = gridmanager.GetCoordinatesFromPosition(tile.transform.position);
        gridmanager.BlockNode(coordinates);
    }
}
