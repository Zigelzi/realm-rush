using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateHandler : MonoBehaviour
{
    private TextMeshPro cubeText;
    private Vector2Int coordinates = new Vector2Int();
    // Start is called before the first frame update
    void Awake()
    {
        cubeText = GetComponent<TextMeshPro>();
        SetCubeText();
        
    }

    void Update()
    {
       if (!Application.isPlaying)
        {
            SetCubeText();
            SetGameObjectNameToCoordinate();
        }
    }

    private void SetCubeText()
    {
        float grizSize = UnityEditor.EditorSnapSettings.move.x;
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / grizSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / grizSize); 
        cubeText.text = $"{coordinates.x}, {coordinates.y}";
    }

    private void SetGameObjectNameToCoordinate()
    {
        gameObject.transform.parent.name = $"Tile_{coordinates.x}_{coordinates.y}";
    }
}
