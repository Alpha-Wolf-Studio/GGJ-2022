using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private LayerMask movableMask = default;
    [SerializeField] private float speed = .25f;
    [SerializeField] private List<Transform> waypoints;

    private int currentWaypoint = 0;
    private float currentPosition = 0;

    private PlayerController player = null;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Utils.CheckLayerInMask(movableMask, other.gameObject.layer))
        {
            player = other.transform.parent.gameObject.GetComponent<PlayerController>();
            player.transform.SetParent(transform);
        }
    }

    /*private void OnTriggerStay2D(Collider2D other)
    {
        if (Utils.CheckLayerInMask(movableMask, other.gameObject.layer))
        {
            if (player != null)
            {
                
            }
        }
    }*/

    private void OnTriggerExit2D(Collider2D other)
    {
        if (Utils.CheckLayerInMask(movableMask, other.gameObject.layer))
        {
            player.transform.SetParent(null);
            player = null;
        }
    }
}
