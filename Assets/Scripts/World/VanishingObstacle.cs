using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishingObstacle : MonoBehaviour, IObstacle
{

    SpriteRenderer rend;
    Collider2D col;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    public void Activate() 
    {
        if(rend) rend.enabled = true;
        if(col) col.isTrigger = false;
    }

    public void Disactivate()
    {
        if (rend) rend.enabled = false;
        if (col) col.isTrigger = true;
    }

}
