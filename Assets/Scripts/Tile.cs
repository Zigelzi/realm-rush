using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    [SerializeField] bool hasTower = false;
    [SerializeField] Tower towerOnTile; // Tower on the tile
    [SerializeField] Tower towerPrefab;


    public bool IsPlaceable { get { return isPlaceable; } }
    public bool HasTower { get { return hasTower; } set { hasTower = value; } }
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
