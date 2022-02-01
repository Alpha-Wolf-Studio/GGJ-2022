using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusheableObstacle : Obstacle
{
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Activate()
    {
        if (rb)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = .0f;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    public override void Disactivate()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = .0f;
        if (rb) rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
