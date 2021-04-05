using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{

    [SerializeField][Range(0f, 5f)] float movementSpeed = 1f; // Seconds

    List<Node> path = new List<Node>();

    Enemy enemy;
    GridManager gridManager;
    Pathfinder pathFinder;

    IEnumerator followPath;

    // Start is called before the first frame update
    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);     
    }

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<Pathfinder>();
    }

    void OnDisable()
    {
        ReturnToStart();
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        followPath = FollowPath();
        // Clear the path to be safe that it's empty list.
        if (resetPath)
        {
            coordinates = pathFinder.StartCoordinates;
        } else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear(); 
        path = pathFinder.GetNewPath(coordinates);
        if (path.Count > 0)
        {  
            StartCoroutine(followPath);
        }

    }

    void ReturnToStart()
    {
        Vector3 startingPosition = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
        transform.position = startingPosition;
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startingPosition = transform.position;
            Vector3 destinationPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            transform.LookAt(destinationPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * movementSpeed;
                transform.position = Vector3.Lerp(startingPosition, destinationPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        // When last waypoint in the path is reached, destroy the gameobject and steal gold
        gameObject.SetActive(false);
        enemy.StealGold();
    }
}
