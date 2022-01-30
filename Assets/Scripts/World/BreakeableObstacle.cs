using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakeableObstacle : MonoBehaviour
{
    private bool broken = false;

    [SerializeField] List<GameObject> healthyGameObjects;
    [SerializeField] List<GameObject> brokenGameObjects;

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
        boxCollider.enabled = false;
        for (int i = 0; i < healthyGameObjects.Count; i++)
        {
            healthyGameObjects[i].SetActive(false);
        }
        for (int i = 0; i < brokenGameObjects.Count; i++)
        {
            brokenGameObjects[i].SetActive(true);
        }
    }
}
