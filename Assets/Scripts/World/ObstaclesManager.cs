using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{

    public Action OnSpikeTouched;

    [SerializeField] GameObject yingObjectsBase;
    IObstacle[] yingObjects;

    [SerializeField] GameObject yangObjectsBase;
    IObstacle[] yangObjects;

    [SerializeField] GameObject boxesObjectsBase;
    IObstacle[] boxesObjects;

    [SerializeField] GameObject spikesObjectsBase;
    Spike[] spikes;

    private void Start()
    {
        GameManager.Get().OnColorChange += ChangeState;
        yingObjects = yingObjectsBase.GetComponentsInChildren<IObstacle>();
        yangObjects = yangObjectsBase.GetComponentsInChildren<IObstacle>();
        boxesObjects = boxesObjectsBase.GetComponentsInChildren<IObstacle>();
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
            foreach (IObstacle o in yangObjects)
            {
                o.Disactivate();
            }
            foreach (IObstacle o in boxesObjects)
            {
                o.Activate();
            }
            foreach (IObstacle o in yingObjects) 
            {
                o.Activate();
            }
            foreach (var spike in spikes)
            {
                spike.Activate();
            }
        }
        else 
        {
            foreach (IObstacle o in yangObjects)
            {
                o.Activate();
            }
            foreach (IObstacle o in yingObjects)
            {
                o.Disactivate();
            }
            foreach (IObstacle o in boxesObjects)
            {
                o.Disactivate();
            }
            foreach (var spike in spikes)
            {
                spike.Disactivate();
            }
        }
    }

    void SpikeTouch() 
    {
        OnSpikeTouched?.Invoke();
    }

}
