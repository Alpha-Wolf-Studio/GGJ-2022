using UnityEngine;
public class Pickeable : Obstacle
{

    [SerializeField] float speed = .2f;
    [SerializeField] float movementOffset = .25f;

    Vector3 startPosUp;
    Vector3 startPosDown;
    bool goingUp = true;

    [SerializeField] ParticleSystem ownParticleSystem;
    
    private void Start()
    {
        Vector3 pos = transform.position;
        startPosUp = new Vector3(pos.x, pos.y + movementOffset, pos.z);
        startPosDown = new Vector3(pos.x, pos.y - movementOffset, pos.z);
    }
    private void Update()
    {
        if (goingUp) 
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
            if(Vector3.Distance(transform.position, startPosUp) < 0.05) goingUp = false;
        }
        else 
        {
            transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
            if (Vector3.Distance(transform.position, startPosDown) < 0.05) goingUp = true;
        }
    }
    public override void Activate()
    {
        var module = ownParticleSystem.main;
        ParticleSystem.Particle[] aliveParticles;
        aliveParticles = new ParticleSystem.Particle[module.maxParticles];
        int numberOfAliveParticles = ownParticleSystem.GetParticles(aliveParticles);
        module.startColor = Utils.OniWhite;
        for (int i = 0; i < numberOfAliveParticles; i++)
        {
            aliveParticles[i].startColor = Utils.OniWhite;
        }
        ownParticleSystem.SetParticles(aliveParticles, numberOfAliveParticles);
    }
    public override void Disactivate()
    {
        var module = ownParticleSystem.main;
        ParticleSystem.Particle[] aliveParticles;
        aliveParticles = new ParticleSystem.Particle[module.maxParticles];
        int numberOfAliveParticles = ownParticleSystem.GetParticles(aliveParticles);
        module.startColor = Utils.OniBlack;
        for (int i = 0; i < numberOfAliveParticles; i++)
        {
            aliveParticles[i].startColor = Utils.OniBlack;
        }
        ownParticleSystem.SetParticles(aliveParticles, numberOfAliveParticles);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource audioSource = GameObject.FindGameObjectWithTag("ExtraAudio").GetComponent<AudioSource>();
        if (audioSource) audioSource.Play();
        GameManager.Get().PlayerPickUp();
        Destroy(this.gameObject);
    }
}
