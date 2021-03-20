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
    private bool IsBuildable(Tile tile)
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

    private void Demolish()
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
            Instantiate(tower, tile.transform.position, tile.transform.rotation, tile.transform);
            bank.Withdraw(buildCost);
            buildingSuccesful = true;
            return buildingSuccesful;
        }

        buildingSuccesful = false;
        return buildingSuccesful;
    }
}
