using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakeableObstacle : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid = null;
    [SerializeField] private BoxCollider2D boxCollider = null;

    private bool broken = false;

    public void Break()
    {
        if (broken)
            return;

        rigid.isKinematic = true;
        boxCollider.isTrigger = true;
        //animacion de romper
        //destruir gameobject o solo desactivarlo?
    }
}
