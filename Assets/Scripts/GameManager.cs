using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public LevelPrefabs prefabs;
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
        player.OnSpikeTouch+= PlayerDeath;
        playerLastSavedPosition = player.transform.position;
    }

    public void InvertColor() 
    {
        yingState = !yingState;
        OnColorChange?.Invoke(yingState);
    }

    private void PlayerDeath() 
    {
        if (!player.Death)
        {
            player.Dead(true);

            Invoke(nameof(PlayerMoveToSpawn), 1.5f);
        }
    }

    private void PlayerMoveToSpawn()
    {
        player.transform.position = playerLastSavedPosition;
        player.Dead(false);
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

[Serializable]
public class LevelPrefabs
{
    public GameObject checkPoint;
    public GameObject currency;
    public GameObject fakeGround;
    public GameObject platform;
}