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

    [SerializeField] GameObject pickeableObjectsBase;
    Pickeable[] pickeables;

    [SerializeField] GameObject fakeGroundObjectsBase;
    FakeGroundObstacle[] fakeGrounds;

    private void Start()
    {
        GameManager.Get().OnColorChange += ChangeState;
        yingObjects = yingObjectsBase.GetComponentsInChildren<IObstacle>();
        yangObjects = yangObjectsBase.GetComponentsInChildren<IObstacle>();
        boxesObjects = boxesObjectsBase.GetComponentsInChildren<IObstacle>();
        spikes = spikesObjectsBase.GetComponentsInChildren<Spike>();
        pickeables = pickeableObjectsBase.GetComponentsInChildren<Pickeable>();
        fakeGrounds = fakeGroundObjectsBase.GetComponentsInChildren<FakeGroundObstacle>();
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
            foreach (var pickeable in pickeables)
            {
                if(pickeable != null) pickeable.Activate();
            }
            foreach (var fakeGround in fakeGrounds)
            {
                if (fakeGround != null) fakeGround.Activate();
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
            foreach (var pickeable in pickeables)
            {
                if (pickeable != null) pickeable.Disactivate();
            }
            foreach (var fakeGround in fakeGrounds)
            {
                if (fakeGround != null) fakeGround.Disactivate();
            }
        }
    }

    void SpikeTouch() 
    {
        OnSpikeTouched?.Invoke();
    }

}
