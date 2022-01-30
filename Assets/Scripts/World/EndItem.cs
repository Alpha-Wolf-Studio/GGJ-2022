using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndItem : MonoBehaviour
{

    [SerializeField] List<GameObject> currencyActiveGameObjects;
    [SerializeField] List<GameObject> currencyInActiveGameObjects;
    [SerializeField] int currencyToEndGame = 5;
    int currentCurrencyRetrieved = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int newAmount = GameManager.Get().GetCurrentPickeablesAmount();
        if (newAmount != currentCurrencyRetrieved) 
        {
            currentCurrencyRetrieved = newAmount;
            if(currencyActiveGameObjects.Count >= currentCurrencyRetrieved) 
            {
                for (int i = 0; i < currentCurrencyRetrieved; i++)
                {
                    currencyActiveGameObjects[i].SetActive(true);
                    currencyInActiveGameObjects[i].SetActive(false);
                }
            }
            if(currentCurrencyRetrieved >= currencyToEndGame) 
            {
                GameManager.Get().EndGame();
            }
        }
    }
}
