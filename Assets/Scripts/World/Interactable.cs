using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    [SerializeField] GameObject doorToOpen;

    bool interacted = false;

    public void Interact() 
    {
        if (interacted || !doorToOpen) return;
        Destroy(doorToOpen);
        interacted = true;
    }
}
