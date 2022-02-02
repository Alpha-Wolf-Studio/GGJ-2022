using System;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class LevelManager : MonoBehaviourSingleton<LevelManager>
{
    [SerializeField] private ParentGroup parents = new ParentGroup();
    [Header("Modificable:")]
    [SerializeField] private List<GameObject> currencies = new List<GameObject>();

    private int currenciesCount;

    private void Start()
    {
        
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (currencies.Count != currenciesCount)
        {
            currenciesCount = currencies.Count;
            Instantiate(GameManager.Get().prefabs.currency, Vector3.zero, Quaternion.identity, parents.platforms);
        }
    }
#endif
}

[Serializable]
public class ParentGroup
{
    public Transform platforms;
    public Transform currencies;
}