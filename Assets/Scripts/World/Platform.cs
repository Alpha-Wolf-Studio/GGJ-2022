using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class Platform : MonoBehaviour
{
    public readonly float cornerWidth = 0.56f;
    public readonly float midWidth = 0.57f;
    public readonly float grassHeight = 0.28f;
    public readonly float grassTileHeight = 0.21f;
    public readonly float spikeOffSetHeight = -0.28f;

    [SerializeField] private GameObject pfMidPlatform; 
    [SerializeField] private GameObject pfCornerPlatform;
    [SerializeField] private GameObject pfGrass;
    [SerializeField] private GameObject pfSpike;
    private Transform lastPlatform;

    [Header("Configurable:")]
    [SerializeField] private bool updateSettings = false;
    [SerializeField] private int lenght = 0;
    [SerializeField] private bool hasGrass = true;
    [SerializeField] private bool hasCornerLeft= true;
    [SerializeField] private bool hasCornerRight = true;
    [SerializeField] private bool hasSpikeUp = false;
    [SerializeField] private bool hasSpikeDown = false;
    [SerializeField] private Vector2 position;
    public Configurable configurable;
#if UNITY_EDITOR
    private void Update()
    {
        SetPosition();

        if (!updateSettings) return;
        updateSettings = false;
        
        ClearList();
        InstantiateNewPlatforms();

        if (hasSpikeUp)
        {
            GameObject spikeObject = Instantiate(pfSpike, transform.position, Quaternion.identity, transform);
            spikeObject.AddComponent<SpriteRenderer>();
            spikeObject.GetComponent<SpriteRenderer>().color = Color.clear;

            SpikeSettings spikeSetting = spikeObject.GetComponent<SpikeSettings>();
            spikeSetting.lenght = 1;
            spikeSetting.flipY = false;

            Vector3 pos = lastPlatform.localPosition / 2;
            pos.y = -spikeOffSetHeight * 3 / 2;
            pos.z = 0;
            spikeSetting.transform.localPosition = pos;
        }
        if (hasSpikeDown)
        {
            GameObject spikeObject = Instantiate(pfSpike, transform.position, Quaternion.identity, transform);
            spikeObject.AddComponent<SpriteRenderer>();
            spikeObject.GetComponent<SpriteRenderer>().color = Color.clear;

            SpikeSettings spikeSetting = spikeObject.GetComponent<SpikeSettings>();
            spikeSetting.lenght = 1;
            spikeSetting.flipY = true;

            Vector3 pos = lastPlatform.localPosition / 2;
            pos.y = spikeOffSetHeight;
            pos.z = 0;
            spikeSetting.transform.localPosition = pos;
        }
    }
    void ClearList()
    {
        SpriteRenderer[] sr = GetComponentsInChildren<SpriteRenderer>();
        List<GameObject> gos = new List<GameObject>();
        for (int i = 0; i < sr.Length; i++)
        {
            gos.Add(sr[i].gameObject);
        }

        for (int i = 0; i < gos.Count; i++)
        {
            DestroyImmediate(gos[i]);
        }
        gos.Clear();
    }
    void SetPosition()
    {
        if ((Vector2) transform.position != position)
        {
            transform.position = position;
        }
    }
    void InstantiateNewPlatforms()
    {
        if (hasGrass)
        {
            SetGrass();
        }
        if (hasCornerLeft)
        {
            GameObject go = Instantiate(pfCornerPlatform, transform.position, Quaternion.identity, transform);
            lastPlatform = go.transform;
        }
        for (int i = 0; i < lenght; i++)
        {
            GameObject go = Instantiate(pfMidPlatform, Vector3.zero, Quaternion.identity, transform);
            go.transform.localPosition = new Vector3(cornerWidth + midWidth * i, 0, 0);
            go.name = "Platform(" + i + ")";
            lastPlatform = go.transform;
        }
        if (hasCornerRight)
        {
            Transform right = Instantiate(pfCornerPlatform, Vector3.zero, new Quaternion(0, 1, 0, 0), transform).transform;
            float width = cornerWidth + midWidth * lenght;
            right.localPosition = new Vector3(width, 0, 0);
            lastPlatform = right;
        }
    }
    void SetGrass()
    {
        Vector3 pos = new Vector3(transform.position.x + cornerWidth + midWidth * lenght, transform.position.y + grassHeight, 0);
        SpriteRenderer grass = Instantiate(pfGrass, pos, Quaternion.identity, transform).GetComponent<SpriteRenderer>();

        grass.size = new Vector2(cornerWidth * 2 + lenght * midWidth, grassTileHeight);
    }
#endif
}
public class Configurable
{
    
}