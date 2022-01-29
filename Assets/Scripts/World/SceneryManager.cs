using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneryManager : MonoBehaviour
{
    [SerializeField] GameObject groundBaseObject;
    SpriteRenderer[] groundRenderers;
    [SerializeField] SpriteRenderer backgroundRenderer;
    [SerializeField] GameObject textBaseObject;
    TextMeshPro[] textMeshes;

    private void Awake()
    {
        groundRenderers = groundBaseObject.GetComponentsInChildren<SpriteRenderer>();
        textMeshes = textBaseObject.GetComponentsInChildren<TextMeshPro>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Get().OnColorChange += ChangeColor;
    }

    public void ChangeColor(bool yingState) 
    {
        if (yingState) 
        {
            foreach(SpriteRenderer r in groundRenderers) 
            {
                r.color = Color.white;
            }
            foreach (var texts in textMeshes)
            {
                texts.color = Color.white;
            }
            backgroundRenderer.color = Color.black;
        }
        else 
        {
            foreach (SpriteRenderer r in groundRenderers)
            {
                r.color = Color.black;
            }
            foreach (var texts in textMeshes)
            {
                texts.color = Color.black;
            }
            backgroundRenderer.color = Color.white;

        }
    }
}
