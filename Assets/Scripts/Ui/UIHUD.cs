using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text pearlsText = null;

    private void Start()
    {
        GameManager.Get().OnCurrencyChange += PearlsUpdate;
    }

    private void PearlsUpdate(int pearlsValue)
    {
        pearlsText.text = pearlsValue.ToString();
    }
}
