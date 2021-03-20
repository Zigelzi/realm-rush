using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class Waypoint : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color placedColor = Color.gray;

    private TextMeshPro tileLabel;
    private Vector2Int coordinates = new Vector2Int();

    private Tile tile;
    // Start is called before the first frame update
    void Awake()
    {
        
        tileLabel = GetComponent<TextMeshPro>();
        tile = GetComponentInParent<Tile>();

        tileLabel.enabled = false;
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

        SetTileColour();
    }

    private void SetTileText()
    {
        float grizSize = UnityEditor.EditorSnapSettings.move.x;
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / grizSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / grizSize); 
        tileLabel.text = $"{coordinates.x}, {coordinates.y}";
    }

    private void SetTileColour()
    {
        if (tile.IsPlaceable && !tile.HasBuilding)
        {
            tileLabel.color = defaultColor;
        } else
        {
            tileLabel.color = placedColor;
        }
    }

    private void ToggleVisibility()
    {
        tileLabel.enabled = !tileLabel.IsActive();
    }

    private void SetGameObjectName()
    {
        gameObject.transform.parent.name = $"Tile_{coordinates.x}_{coordinates.y}";
    }
}
