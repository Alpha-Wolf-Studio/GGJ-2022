using System;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour, IObstacle
{
    public Action OnTouch;

    SpriteRenderer rend;
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    public void Activate()
    {
        rend.color = Utils.OniWhite;
    }

    public void Disactivate()
    {
        rend.color = Utils.OniBlack;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTouch?.Invoke();
    }
}
