using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusheableObstacle : MonoBehaviour, IObstacle
{
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Activate()
    {
        if (rb)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = .0f;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    public void Disactivate()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = .0f;
        if (rb) rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
