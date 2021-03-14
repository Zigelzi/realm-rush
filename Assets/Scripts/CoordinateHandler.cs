using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateHandler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color placedColor = Color.gray;

    private TextMeshPro tileText;
    private Vector2Int coordinates = new Vector2Int();

    private Tile tile;
    // Start is called before the first frame update
    void Awake()
    {
        tileText = GetComponent<TextMeshPro>();
        tile = GetComponentInParent<Tile>();
        SetTileText();
        
    }

    void Update()
    {
       if (!Application.isPlaying)
        {
            SetTileText();
            SetGameObjectNameToCoordinate();
        }

       if (Debug.isDebugBuild  && Input.GetKeyUp(KeyCode.Z))
        {
            ToggleCoordinates();
        }

        SetTileColour();
    }

    private void SetTileText()
    {
        float grizSize = UnityEditor.EditorSnapSettings.move.x;
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / grizSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / grizSize); 
        tileText.text = $"{coordinates.x}, {coordinates.y}";
    }

    private void SetTileColour()
    {
        if (tile.IsPlaceable && !tile.HasBuilding)
        {
            tileText.color = defaultColor;
        } else
        {
            tileText.color = placedColor;
        }
    }

    private void ToggleCoordinates()
    {
        tileText.enabled = !tileText.enabled;
    }

    private void SetGameObjectNameToCoordinate()
    {
        gameObject.transform.parent.name = $"Tile_{coordinates.x}_{coordinates.y}";
    }
}
