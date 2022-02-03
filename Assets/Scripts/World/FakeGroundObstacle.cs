using UnityEngine;
[ExecuteInEditMode]
public class FakeGroundObstacle : Obstacle
{
    private SpriteRenderer rend;
    [SerializeField] private GameObject pfSpike;
    private GameObject mySpike;
    private Vector2 spikeOffSet = new Vector2(-0.64f, -0.05f);

    [Header("Configurable:")]
    [SerializeField] private bool updateSettings;
    [SerializeField] private bool hasSpike;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (!updateSettings) return;
        updateSettings = false;

        if (hasSpike)
        {
            if (mySpike) return;

            mySpike = Instantiate(pfSpike, transform.position, Quaternion.identity, transform);
            mySpike.transform.localScale = Vector3.one / 2;
            mySpike.transform.localPosition = spikeOffSet;

            SpikeSettings spikeSettings = mySpike.GetComponent<SpikeSettings>();
            spikeSettings.lenght = 5;
            spikeSettings.updateSettings = true;
        }
        else
        {
            DestroyImmediate(mySpike);
        }
    }
#endif
    public override void Activate()
    {
        rend.color = Utils.OniWhite;
    }
    public override void Disactivate()
    {
        rend.color = Utils.OniBlack;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}