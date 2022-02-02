using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class Platform : MonoBehaviour
{
    public GameObject midPlatform; 
    public readonly float cornerWidth = 0.56f;
    public readonly float midWidth = 0.57f;
    public readonly float grassHeight = 0.28f;
    public readonly float grassTileHeight = 0.21f;

    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private SpriteRenderer grass;

    private List<GameObject> platformMid = new List<GameObject>();
    [Header("Configurable:")]
    [SerializeField] private bool updateSettings = false;
    [SerializeField] private int lenght = 0;
    
#if UNITY_EDITOR
    private void Update()
    {
        if (!updateSettings) return;

        updateSettings = false;
        grass.size = new Vector2(cornerWidth * 2 + lenght * midWidth, grassTileHeight);
        
        ClearList();
        for (int i = 0; i < lenght; i++)
        {
            GameObject go = Instantiate(midPlatform, Vector3.zero, Quaternion.identity, transform);
            go.transform.localPosition = new Vector3(cornerWidth + midWidth * i, 0, 0);
            go.name = "Platform(" + i + ")";
            platformMid.Add(go);
        }

        float width = cornerWidth + midWidth * lenght;
        right.localPosition = new Vector3(width, 0, 0);
        grass.transform.localPosition = new Vector3(width / 2, grassHeight, 0);
        
    }
    void ClearList()
    {
        for (int i = 0; i < platformMid.Count; i++)
        {
            GameObject platform = platformMid[i];
            DestroyImmediate(platform);
        }

        platformMid.Clear();
    }
#endif
}