using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakeableObstacle : MonoBehaviour
{
    private bool broken = false;

    private BoxCollider2D boxCollider = null;
    private Animator animator = null;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        //animator = GetComponent<Animator>();
    }

    public void Break()
    {
        if (broken)
            return;

        broken = true;
        boxCollider.isTrigger = true;
        //animacion de romper
        //destruir gameobject o solo desactivarlo?
        DestroyGO();
    }

    public void DestroyGO()
    {
        Destroy(gameObject);
    }
}
