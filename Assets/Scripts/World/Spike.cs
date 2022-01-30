using System;
using UnityEngine;
public class Spike : MonoBehaviour, IObstacle
{
    public Action OnTouch;
    
    public void Activate()
    {
    }
    public void Disactivate()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTouch?.Invoke();
    }
}