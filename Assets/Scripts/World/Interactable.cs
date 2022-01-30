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

    public void Interact() 
    {
        if (interacted || !doorToOpen) return;
        Destroy(doorToOpen);
        interacted = true;
        RotateSitck();
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
