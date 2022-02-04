using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class BreakeableObstacle : MonoBehaviour
{
    private bool broken = false;

    [SerializeField] List<GameObject> healthyGameObjects;
    [SerializeField] List<GameObject> brokenGameObjects;
    public GameObject[] blockColliders;
    private BoxCollider2D boxCollider = null;
    private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    [Header("Configurable:")]
    [SerializeField] private bool updateSettings = false;
    [SerializeField] private bool blockUp = false;
    [SerializeField] private bool blockDown = false;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (!updateSettings) return;
        updateSettings = false;

        blockColliders[0].SetActive(blockUp);
        blockColliders[1].SetActive(blockDown);
    }
#endif
    public void Break()
    {
        if (broken)
            return;

        if (audioSource)
        {
            if (audioClip)
                audioSource.clip = audioClip;
            audioSource.Play();
        }

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