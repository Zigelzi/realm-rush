using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class Waypoint : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color placedColor = Color.gray;

    TextMeshPro tileLabel;
    Tile tile;
    Vector2Int coordinates = new Vector2Int();

    
    // Start is called before the first frame update
    void Awake()
    {
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
        if (tile.IsPlaceable && !tile.HasTower)
        {
            tileLabel.color = defaultColor;
        } else
        {
            tileLabel.color = placedColor;
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
