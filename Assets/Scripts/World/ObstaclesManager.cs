using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    [SerializeField] GameObject yingObjectsBase;
    Obstacle[] yingObjects;

    [SerializeField] GameObject yangObjectsBase;
    Obstacle[] yangObjects;

    private void Start()
    {
        Gamemanager.Get().OnColorChange += ChangeState;
        yingObjects = yingObjectsBase.GetComponentsInChildren<Obstacle>();
        yangObjects = yangObjectsBase.GetComponentsInChildren<Obstacle>();
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

}
