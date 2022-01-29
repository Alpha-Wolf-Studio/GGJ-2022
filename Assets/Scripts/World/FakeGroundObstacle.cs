using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeGroundObstacle : MonoBehaviour, IObstacle
{
    [SerializeField] SpriteRenderer baseRenderer;
    [SerializeField] SpriteRenderer bgRenderer;

    public void Activate()
    {
        baseRenderer.color = Utils.OniWhite;
    }

    public void Disactivate()
    {
        baseRenderer.color = Utils.OniWhite;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        baseRenderer.enabled = false;
        bgRenderer.enabled = false;
    }
}
