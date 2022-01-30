using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private LayerMask movableMask = default;
    [SerializeField] private float speed = .25f;
    [SerializeField] private List<Transform> waypoints;

    private Vector3 startPosition = Vector3.zero;
    private int currentWaypoint = 0;
    private float currentPosition = 0;
    private bool exitPlayer = false;

    private PlayerController player = null;

    private void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        currentPosition += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startPosition, waypoints[currentWaypoint].position, currentPosition);
        if(currentPosition > 1)
        {
            startPosition = transform.position;
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
            exitPlayer = false;
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
            if (player != null)
            {
                Invoke(nameof(ExitPlayerParent), 0.05f);
                exitPlayer = true;
            }
        }
    }

    private void ExitPlayerParent()
    {
        if (exitPlayer)
        {
            player.transform.SetParent(null);
        }
    }
}
