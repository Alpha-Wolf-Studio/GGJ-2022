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
    private AudioSource audioSource;
    public AudioClip audio02;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Interact() 
    {
        if (interacted || !doorToOpen) return;
        if (audioSource) audioSource.Play();
        Invoke(nameof(ReproduceSecondAudio), 1.1f);
        Destroy(doorToOpen);
        interacted = true;
        RotateSitck();
    }
    private void ReproduceSecondAudio()
    {
        if (!audioSource || !audio02) return;

        audioSource.clip = audio02;
        audioSource.Play();
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
