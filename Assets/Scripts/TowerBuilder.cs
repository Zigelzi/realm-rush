using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    [SerializeField] GameObject buildObjectPrefab;

    
    private GameObject tower;
    private Tile tile;

    void Start()
    {
        tile = GetComponent<Tile>();    
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            DestroyTower();
        }
    }

    private void OnMouseDown()
    {
        BuildTower();
    }

    private void BuildTower()
    {
        if (IsBuildable())
        {
            tower = Instantiate(buildObjectPrefab, transform.position, transform.rotation, transform);
            tile.HasBuilding = true;
        }
    }

    private bool IsBuildable()
    {
        if (tile.IsPlaceable && 
            buildObjectPrefab != null &&
            !tile.HasBuilding)
        {
            return true;
        } else
        {
            return false;
        }
    }

    private void DestroyTower()
    {
        if (tower != null && tile.HasBuilding)
        {
            Destroy(tower);
            tile.HasBuilding = false;
        }
    }
}
