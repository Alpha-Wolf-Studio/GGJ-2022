using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeGroundObstacle : MonoBehaviour, IObstacle
{
    SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    public void Activate()
    {
        rend.color = Utils.OniWhite;
    }

    public void Disactivate()
    {
        rend.color = Utils.OniBlack;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
