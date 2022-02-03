using System.Collections.Generic;
using UnityEngine;

public class BreakeableObstacle : MonoBehaviour
{
    private bool broken = false;

    [SerializeField] List<GameObject> healthyGameObjects;
    [SerializeField] List<GameObject> brokenGameObjects;

    private BoxCollider2D boxCollider = null;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    public void Break()
    {
        if (broken)
            return;

        if (audioSource) audioSource.Play();
        broken = true;
        boxCollider.enabled = false;
        for (int i = 0; i < healthyGameObjects.Count; i++)
        {
            healthyGameObjects[i].SetActive(false);
        }
        for (int i = 0; i < brokenGameObjects.Count; i++)
        {
            brokenGameObjects[i].SetActive(true);
        }
    }
}