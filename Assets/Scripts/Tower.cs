using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
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
            Demolish();
        }
    }

    private void OnMouseDown()
    {
        Build();
    }

    private void Build()
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

    private void Demolish()
    {
        if (tower != null && tile.HasBuilding)
        {
            Destroy(tower);
            tile.HasBuilding = false;
        }
    }
}
