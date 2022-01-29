using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviourSingleton<GameManager>
{

    public PlayerController GetPlayer() => player;

    [SerializeField] PlayerController player;
    [SerializeField] ObstaclesManager obstaclesManager;

    Vector3 playerLastSavedPosition;

    bool yingState = true;
    public Action<bool> OnColorChange;

    private void Start()
    {
        obstaclesManager.OnSpikeTouched += PlayerTouchedSpike;
        playerLastSavedPosition = player.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            InvertColor();
        }
    }

    public void InvertColor() 
    {
        yingState = !yingState;
        OnColorChange?.Invoke(yingState);
    }

    void PlayerTouchedSpike() 
    {
        player.transform.position = playerLastSavedPosition;
    }

    public void NewPlayerSavedPosition(Vector3 newPosition) 
    {
        playerLastSavedPosition = newPosition;
    }
}
