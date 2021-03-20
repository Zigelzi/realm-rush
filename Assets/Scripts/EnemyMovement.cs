using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Tile> path = new List<Tile>();
    [SerializeField][Range(0f, 5f)] float movementSpeed = 1f; // Seconds

    private Enemy enemy;

    // Start is called before the first frame update
    void OnEnable()
    {
        enemy = GetComponent<Enemy>();
        FindPath();
        ReturnToStart();
        if (path.Count > 0)
        {
            StartCoroutine(FollowPath());
        }
        
    }

    void OnDisable()
    {
        if (path != null)
        {
            ReturnToStart();
        }
    }

    private void FindPath()
    {
        // Clear the path to be safe that it's empty list.
        path.Clear();
        GameObject pathParent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform child in pathParent.transform)
        {
            Tile singleTile = child.GetComponent<Tile>();
            if (singleTile != null)
            {
                path.Add(singleTile);
            }
        }
    }

    private void ReturnToStart()
    {
            transform.position = path[0].transform.position;        
    }

    IEnumerator FollowPath()
    {
        foreach(Tile waypoint in path)
        {
            Vector3 startingPosition = transform.position;
            Vector3 destinationPosition = waypoint.transform.position;
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
