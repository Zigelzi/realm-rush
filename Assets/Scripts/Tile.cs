using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    [SerializeField] private bool hasBuilding = false;
    public bool IsPlaceable { get { return isPlaceable; } }
    public bool HasBuilding { get { return hasBuilding; } set { hasBuilding = value; } }
    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            Debug.Log("Placing tower allowed");
        } else
        {
            Debug.Log("Not allowed to place tower here!");
        }
    }
}
