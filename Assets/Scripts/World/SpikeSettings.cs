using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class SpikeSettings : MonoBehaviour
{
    private float spikeWidth = 0.7f;
    private List<GameObject> spikes = new List<GameObject>();
    [SerializeField] private GameObject spike;
    [SerializeField] private List<Sprite> spritesList = new List<Sprite>();

    [Header("Configurable:")] 
    public bool updateSettings;
    public int lenght = 1;
#if UNITY_EDITOR
    void Update()
    {
        if(!updateSettings) return;
        updateSettings = false;

        ClearList();
        for (int i = 0; i < lenght; i++)
        {
            GameObject go = Instantiate(spike, Vector3.zero, Quaternion.identity, transform);
            go.transform.localPosition = new Vector3(spikeWidth* i, 0, 0);
            go.name = "Spike(" + i + ")";

            SpriteRenderer goRenderer = go.GetComponent<SpriteRenderer>();
            int randomIndezSprite = Random.Range(0, spritesList.Count);
            goRenderer.sprite = spritesList[randomIndezSprite];
            goRenderer.flipX = Random.Range(0, 2) == 1;

            spikes.Add(go);
        }
    }
    void ClearList()
    {
        for (int i = 0; i < spikes.Count; i++)
        {
            GameObject platform = spikes[i];
            DestroyImmediate(platform);
        }

        spikes.Clear();
    }
#endif
}