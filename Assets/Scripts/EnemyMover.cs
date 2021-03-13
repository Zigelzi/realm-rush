using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();

    [SerializeField][Range(0f, 5f)] float movementSpeed = 1f; // Seconds

    // Start is called before the first frame update
    void Start()
    {
        if (path.Count > 0)
        {
            StartCoroutine(FollowPath());
        }
        
    }

    IEnumerator FollowPath()
    {
        foreach(Waypoint waypoint in path)
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

    void MoveTo(Waypoint destination)
    {
        Vector3 startingPosition = transform.position;
        Vector3 destinationPosition = destination.transform.position;
        float travelPercent = 0f;

        while(travelPercent < 1f)
        {
            travelPercent += Time.deltaTime;
            transform.position = Vector3.Lerp(startingPosition, destinationPosition, travelPercent);
        }

        
    }
}
