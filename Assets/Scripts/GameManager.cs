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
    public Action OnNewCheckpoint;

    bool yingState = true;
    public Action<bool> OnColorChange;
    
    int currentPickeableAmount = 0;
    public int GetCurrentPickeablesAmount() => currentPickeableAmount;
    public Action<int> OnCurrencyChange;
    public Action OnGameEnded;

    private void Start()
    {
        obstaclesManager.OnSpikeTouched += PlayerMoveToSpawn;
        playerLastSavedPosition = player.transform.position;
    }

    public void InvertColor() 
    {
        yingState = !yingState;
        OnColorChange?.Invoke(yingState);
    }

    void PlayerMoveToSpawn() 
    {
        player.transform.position = playerLastSavedPosition;
    }

    public void NewPlayerSavedPosition(Vector3 newPosition) 
    {
        OnNewCheckpoint?.Invoke();
        playerLastSavedPosition = newPosition;
    }

    public void PlayerPickUp() 
    {
        currentPickeableAmount++;
        OnCurrencyChange?.Invoke(currentPickeableAmount);
    }

    public void EndGame() 
    {
        OnGameEnded?.Invoke();
    }

}
