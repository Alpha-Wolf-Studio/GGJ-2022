using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject stick = null;
    [SerializeField] private float rotateZ = 0f;
    [SerializeField] private float transition = 0f;
    [SerializeField] GameObject doorToOpen;

    bool interacted = false;
    private AudioSource audio;
    public AudioClip audio02;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    public void Interact() 
    {
        if (interacted || !doorToOpen) return;
        if (audio) audio.Play();
        Invoke("ReproduceSecondAudio", 1.1f);
        Destroy(doorToOpen);
        interacted = true;
        RotateSitck();
    }
    private void ReproduceSecondAudio()
    {
        if (!audio || !audio02) return;

        audio.clip = audio02;
        audio.Play();
    }
    private void RotateSitck()
    {
        IEnumerator RotateDelay()
        {
            float timer = 0f;
            while (timer < transition)
            {
                timer += Time.deltaTime;
                stick.transform.eulerAngles =
                    Vector3.Lerp(Vector3.zero, new Vector3(0f, 0f, rotateZ), timer / transition);

                yield return new WaitForEndOfFrame();
            }
        }

        StartCoroutine(RotateDelay());
    }
}
