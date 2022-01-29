using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gamemanager : MonoBehaviourSingleton<Gamemanager>
{

    bool YingState = true;
    public Action<bool> OnColorChange;
    // Start is called before the first frame update

    public void InvertColor() 
    {
        YingState = !YingState;
        OnColorChange?.Invoke(YingState);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            InvertColor();
        }
    }
}
