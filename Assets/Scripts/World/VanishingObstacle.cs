using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishingObstacle : Obstacle
{

    SpriteRenderer rend;
    Collider2D col;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    public override void Activate() 
    {
        if(rend) rend.enabled = true;
        if(col) col.isTrigger = false;
    }

    public override void Disactivate()
    {
        if (rend) rend.enabled = false;
        if (col) col.isTrigger = true;
    }

}
