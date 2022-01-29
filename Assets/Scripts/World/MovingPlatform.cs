using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float speed = .25f;
    [SerializeField] List<Transform> waypoints;
    int currentWaypoint = 0;
    float currentPosition = 0;
    void Update()
    {
        currentPosition += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(transform.position, waypoints[currentWaypoint].position, currentPosition);
        if(currentPosition > 1) 
        {
            currentPosition = 0;
            currentWaypoint++;
            if (currentWaypoint > waypoints.Count - 1) currentWaypoint = 0;
        }
    }
}
