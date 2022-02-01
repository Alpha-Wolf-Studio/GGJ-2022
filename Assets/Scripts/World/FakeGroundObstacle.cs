using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeGroundObstacle : Obstacle
{
    SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    public override void Activate()
    {
        rend.color = Utils.OniWhite;
    }

    public override void Disactivate()
    {
        rend.color = Utils.OniBlack;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
