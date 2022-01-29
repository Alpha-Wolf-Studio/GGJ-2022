using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{

    public Action OnSpikeTouched;

    [SerializeField] GameObject yingObjectsBase;
    Obstacle[] yingObjects;

    [SerializeField] GameObject yangObjectsBase;
    Obstacle[] yangObjects;

    [SerializeField] GameObject spikesObjectsBase;
    Spike[] spikes;

    private void Start()
    {
        GameManager.Get().OnColorChange += ChangeState;
        yingObjects = yingObjectsBase.GetComponentsInChildren<Obstacle>();
        yangObjects = yangObjectsBase.GetComponentsInChildren<Obstacle>();
        spikes = spikesObjectsBase.GetComponentsInChildren<Spike>();
        foreach (var spike in spikes)
        {
            spike.OnTouch += SpikeTouch;
        }
    }

    void ChangeState(bool yingState) 
    {
        if (yingState) 
        {
            foreach(Obstacle o in yingObjects) 
            {
                o.Activate();
            }
            foreach (Obstacle o in yangObjects)
            {
                o.Disactivate();
            }
        }
        else 
        {
            foreach (Obstacle o in yangObjects)
            {
                o.Activate();
            }
            foreach (Obstacle o in yingObjects)
            {
                o.Disactivate();
            }
        }
    }

    void SpikeTouch() 
    {
        OnSpikeTouched?.Invoke();
    }

}
