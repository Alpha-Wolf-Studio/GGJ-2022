﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviourSingleton<GameManager>
{

    public PlayerController GetPlayer() => player;

    [SerializeField] PlayerController player;
    [SerializeField] ObstaclesManager obstaclesManager;

    Vector3 playerLastSavedPosition;
    public Action OnNewCheckpoint;

    bool yingState = true;
    public Action<bool> OnColorChange;

    private void Start()
    {
        obstaclesManager.OnSpikeTouched += PlayerTouchedSpike;
        playerLastSavedPosition = player.transform.position;
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
        OnNewCheckpoint?.Invoke();
        playerLastSavedPosition = newPosition;
    }
}
