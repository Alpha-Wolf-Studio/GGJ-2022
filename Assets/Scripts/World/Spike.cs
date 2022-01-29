using System;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public Action OnTouch;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTouch?.Invoke();
    }
}
