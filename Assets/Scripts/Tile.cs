using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class Tile : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    [SerializeField] bool hasTower = false;
    [SerializeField] Tower towerOnTile; // Tower on the tile
    [SerializeField] Tower towerPrefab;

    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.47f, 0.14f);
    [SerializeField] Color startColor = Color.green;
    [SerializeField] Color destinationColor = Color.red;

    GridManager gridManager;
    TextMeshPro tileLabel;
    Vector2Int coordinates = new Vector2Int();


    public bool IsPlaceable { get { return isPlaceable; } }
    public bool HasTower { get { return hasTower; } set { hasTower = value; } }

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        tileLabel = GetComponentInChildren<TextMeshPro>();
        
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

        if (Debug.isDebugBuild && Input.GetKeyUp(KeyCode.C))
        {
            ToggleVisibility();
        }

        SetTileLabelColour();
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

    void SetTileText()
    {
        float grizSize = UnityEditor.EditorSnapSettings.move.x;
        coordinates.x = Mathf.RoundToInt(transform.position.x / grizSize);
        coordinates.y = Mathf.RoundToInt(transform.position.z / grizSize);
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
        gameObject.transform.name = $"Tile_{coordinates.x}_{coordinates.y}";
    }
}
