using System.Collections.Generic;
using UnityEngine;
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float waitingTime = 1f;
    [SerializeField] private float speed = .25f;
    [SerializeField] private List<Transform> waypoints;

    private Vector3 startPosition = Vector3.zero;
    private int currentWaypoint = 0;
    private float currentPosition = 10;
    private bool exitPlayer = false;

    private PlayerController player = null;

    private float firstWaitingTime = 0;
    private bool firstTimeWaiting = true;
    private bool waiting = false;
    private float onTime = 0;

    private void Start()
    {
        firstWaitingTime = Random.Range(1.0f, 3.0f);
        startPosition = transform.position;
    }
    void Update()
    {
        if (firstTimeWaiting)
        {
            onTime += Time.deltaTime;

            if (onTime > firstWaitingTime) 
            {
                firstTimeWaiting = false;
                onTime = 0;
            }
            else
                return;
        }
        else if (waiting) 
        {
            onTime += Time.deltaTime;

            if (onTime > waitingTime)
            {
                waiting = false;
                onTime = 0;
            }
            else
                return;
        }

        currentPosition += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startPosition, waypoints[currentWaypoint].position, currentPosition);
        if(currentPosition > 1)
        {
            startPosition = transform.position;
            currentPosition = 0;
            currentWaypoint++;
            waiting = true;
            if (currentWaypoint > waypoints.Count - 1) currentWaypoint = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Utils.Get().CheckLayerInPlayer(other.gameObject.layer))
        {
            player = other.transform.parent.gameObject.GetComponent<PlayerController>();
            player.transform.SetParent(transform);
            exitPlayer = false;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (Utils.Get().CheckLayerInPlayer(other.gameObject.layer))
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