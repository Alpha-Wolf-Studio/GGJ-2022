using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneryManager : MonoBehaviour
{
    [SerializeField] GameObject groundBaseObject;
    SpriteRenderer[] groundRenderers;
    [SerializeField] SpriteRenderer backgroundRenderer;

    private void Awake()
    {
        groundRenderers = groundBaseObject.GetComponentsInChildren<SpriteRenderer>();
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
            backgroundRenderer.color = Color.black;
        }
        else 
        {
            foreach (SpriteRenderer r in groundRenderers)
            {
                r.color = Color.black;
            }
            backgroundRenderer.color = Color.white;
        }
    }
}
