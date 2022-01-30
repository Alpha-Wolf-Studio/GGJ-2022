using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneryManager : MonoBehaviour
{
    [SerializeField] GameObject groundBaseObject;
    SpriteRenderer[] groundRenderers;
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
                r.color = Utils.OniWhite;
            }
            foreach (var texts in textMeshes)
            {
                texts.color = Utils.OniWhite;
            }
            Camera.main.backgroundColor = Utils.OniBlack;
        }
        else 
        {
            foreach (SpriteRenderer r in groundRenderers)
            {
                r.color = Utils.OniBlack;
            }
            foreach (var texts in textMeshes)
            {
                texts.color = Utils.OniBlack;
            }
            Camera.main.backgroundColor = Utils.OniWhite;
        }
    }
}
