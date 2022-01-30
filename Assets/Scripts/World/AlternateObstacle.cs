using System.Collections;
using UnityEngine;
public class AlternateObstacle : MonoBehaviour, IObstacle
{
    [Header("Setting Obstacle:")]
    [SerializeField] private float visibleTime = 1;
    [SerializeField] private float invisibleTime = 2;
    [SerializeField] private float spawnTime = 0.1f;

    [Header("References:")]
    public bool updateAvailable;
    [SerializeField] private Collider2D coll = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;

    private bool isVisible = false;
    private float onTime = 0;
    private Vector3 startSize = Vector3.one;


    private float waitingTime;
    private float onTimeWaiting = 0;
    private bool isWaiting = true;

    private void Awake()
    {
        if (!coll) coll = GetComponent<Collider2D>();
        if (!spriteRenderer) spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        waitingTime = UnityEngine.Random.Range(0.0f, 3.0f);
        startSize = transform.localScale;
    }
    private void Update()
    {
        if (!updateAvailable) return;

        if (isWaiting)
        {
            onTime += Time.deltaTime;

            if (onTime > waitingTime)
                isWaiting = false;
            else
                return;
        }

        onTime += Time.deltaTime;
        if (isVisible)
        {
            if (onTime > visibleTime)
                StartCoroutine(AlternateSize(false));
        }
        else
        {
            if (onTime > invisibleTime)
                StartCoroutine(AlternateSize(true));
        }
    }
    private void TurnVisibility(bool visibility)
    {
        onTime = 0;
        isVisible = visibility;
    }
    private IEnumerator AlternateSize(bool visibility)
    {
        float onTimeTransition = 0;

        while (onTimeTransition < spawnTime)
        {
            onTimeTransition += Time.deltaTime;
            float lerp = onTimeTransition / spawnTime;
            Vector3 currentSize = visibility ? Vector3.Lerp(Vector3.zero, startSize, lerp) : Vector3.Lerp(startSize, Vector3.zero, lerp);

            transform.localScale = currentSize;
            yield return null;
        }
        TurnVisibility(visibility);
    }
    public void Activate()
    {
        onTime = 0;
    }
    public void Disactivate()
    {

    }
}