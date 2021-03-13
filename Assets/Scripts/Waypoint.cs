using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            Debug.Log("Placing tower allowed");
        } else
        {
            Debug.Log("Not allowed to place tower here!");
        }
        Debug.Log(transform.name);

    }
}
