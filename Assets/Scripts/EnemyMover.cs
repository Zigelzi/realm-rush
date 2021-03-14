using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Tile> path = new List<Tile>();

    [SerializeField][Range(0f, 5f)] float movementSpeed = 1f; // Seconds

    // Start is called before the first frame update
    void Start()
    {
        FindPath();
        if (path.Count > 0)
        {
            StartCoroutine(FollowPath());
        }
        
    }

    private void FindPath()
    {
        GameObject pathParent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform child in pathParent.transform)
        {
            Tile singleTile = child.GetComponent<Tile>();
            path.Add(singleTile);
        }
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
    }
}
