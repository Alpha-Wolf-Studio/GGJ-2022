using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private LayerMask movableMask = default;
    [SerializeField] float speed = .25f;
    [SerializeField] List<Transform> waypoints;
    int currentWaypoint = 0;
    float currentPosition = 0;

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
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Utils.CheckLayerInMask(movableMask, other.gameObject.layer))
        {
            if (player != null)
            {
                //mover personaje
                //player.transform.Translate(transform.position * Time.deltaTime); no funca :c
                //player.Rigid.MovePosition(transform.position * Time.deltaTime); tampoco funca :c
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (Utils.CheckLayerInMask(movableMask, other.gameObject.layer))
        {
            player = null;
        }
    }
}
