using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();

    [SerializeField] float stepTime = 1f; // Seconds

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        foreach(Waypoint waypoint in path)
        {
            MoveTo(waypoint);
            yield return new WaitForSeconds(stepTime);
        }
    }

    void MoveTo(Waypoint destination)
    {
        transform.position = destination.transform.position;
    }
}
