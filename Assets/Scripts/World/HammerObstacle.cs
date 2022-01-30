using UnityEngine;
public class HammerObstacle : MonoBehaviour, IObstacle
{
    private float speed = 50;
    private float limit = 0.7f;
    private Rigidbody2D rb;
    private float waitingTime;
    private float onTime = 0;
    private bool isWaiting = true;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        waitingTime = UnityEngine.Random.Range(0.0f, 3.0f);
        rb.angularVelocity = 200;
    }
    private void Update()
    {
        if (isWaiting)
        {
            onTime += Time.deltaTime;

            if (onTime > waitingTime)
                isWaiting = false;
            else
                return;
        }
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