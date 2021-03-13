using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    [SerializeField] GameObject buildObjectPrefab;
    [SerializeField] bool isPlaceable;

    private bool hasBuilding = false;
    private GameObject tower;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right click");
            DestroyTower();
        }
    }

    private void OnMouseDown()
    {
        BuildTower();
    }

    private void BuildTower()
    {
        if (buildObjectPrefab != null && !hasBuilding)
        {
            tower = Instantiate(buildObjectPrefab, transform.position, transform.rotation, transform);
            hasBuilding = true;
        }
    }

    private void DestroyTower()
    {
        if (tower != null && hasBuilding)
        {
            Destroy(tower);
            hasBuilding = false;
        }
    }
}
