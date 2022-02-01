using System;
using UnityEngine;
public class ObstaclesManager : MonoBehaviour
{
    public Action OnSpikeTouched;

    [SerializeField] GameObject obstacleObjectsBase;
    Obstacle[] obstacleObjects;

    private void Start()
    {
        GameManager.Get().OnColorChange += ChangeState;
        obstacleObjects = FindObjectsOfType<Obstacle>();
    }
    void ChangeState(bool yingState) 
    {
        if (yingState) 
        {
            foreach (Obstacle o in obstacleObjects)
            {
                if (o)
                    o.Activate();
            }
        }
        else 
        {
            foreach (Obstacle o in obstacleObjects)
            {
                if (o)
                    o.Disactivate();
            }
        }
    }
}