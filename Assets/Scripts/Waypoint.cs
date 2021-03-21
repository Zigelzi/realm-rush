using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class Waypoint : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.47f, 0.14f);
    [SerializeField] Color startColor = Color.green;
    [SerializeField] Color destinationColor = Color.red;

    GridManager gridManager;
    TextMeshPro tileLabel;
    Tile tile;
    Vector2Int coordinates = new Vector2Int();

    
    // Start is called before the first frame update
    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        tileLabel = GetComponent<TextMeshPro>();
        tile = GetComponentInParent<Tile>();

        tileLabel.enabled = true;
        SetTileText();
        
    }

    void Update()
    {
       if (!Application.isPlaying)
        {
            SetTileText();
            SetGameObjectName();
        }

       if (Debug.isDebugBuild  && Input.GetKeyUp(KeyCode.C))
        {
            ToggleVisibility();
        }

        SetTileLabelColour();
    }

    void SetTileText()
    {
        float grizSize = UnityEditor.EditorSnapSettings.move.x;
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / grizSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / grizSize); 
        tileLabel.text = $"{coordinates.x}, {coordinates.y}";
    }

    void SetTileLabelColour()
    {
        if (gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinates);

        if (node == null) { return; }

        if (!node.isWalkable)
        {
            tileLabel.color = blockedColor;
        }
        else if (node.isStart)
        {
            tileLabel.color = startColor;
        } 
        else if (node.isDestination)
        {
            tileLabel.color = destinationColor;
        } 
        else if (node.isPath)
        {
            tileLabel.color = pathColor;
        } 
        else if (node.isExplored)
        {
            tileLabel.color = exploredColor;
        }
        else
        {
            tileLabel.color = defaultColor;
        }

        
        
    }

    void ToggleVisibility()
    {
        tileLabel.enabled = !tileLabel.IsActive();
    }

    void SetGameObjectName()
    {
        gameObject.transform.parent.name = $"Tile_{coordinates.x}_{coordinates.y}";
    }
}
