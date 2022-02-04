using System;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class LevelManager : MonoBehaviourSingleton<LevelManager>
{
    [SerializeField] private ParentGroup parents = new ParentGroup();
    List<GameObject> currencies = new List<GameObject>();

    [Header("Modificable:")] 
    [SerializeField] private int currenciesCount;
    private int lastCurrenciesCount;

    private void Start()
    {
        
    }
#if UNITY_EDITOR
    private void Update()
    {
        

    }
#endif
}

[Serializable]
public class ParentGroup
{
    public Transform platforms;
    public Transform currencies;
}