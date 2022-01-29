using UnityEngine;
public class HammerObstacle : MonoBehaviour, IObstacle
{
    private float speed = 50;
    private float limit = 0.7f;
    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        rb.angularVelocity = 200;
    }
    private void Update()
    {
        if (transform.rotation.z < limit && rb.angularVelocity > 0 && rb.angularVelocity < speed)
            rb.angularVelocity = speed;
        else if (transform.rotation.z > limit && rb.angularVelocity < 0 && rb.angularVelocity > -speed)
            rb.angularVelocity = -speed;
    }
    public void Activate()
    {

    }
    public void Disactivate()
    {

    }
}