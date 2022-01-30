using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickeable : MonoBehaviour, IObstacle
{

    [SerializeField] float speed = .2f;
    [SerializeField] float movementOffset = .25f;

    Vector3 startPosUp;
    Vector3 startPosDown;
    bool goingUp = true;

    [SerializeField] ParticleSystem ownParticleSystem;
    private void Start()
    {
        startPosUp = new Vector3(transform.position.x, transform.position.y + movementOffset, transform.position.z);
        startPosDown = new Vector3(transform.position.x, transform.position.y - movementOffset, transform.position.z);
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

    public void Activate()
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

    public void Disactivate()
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
        GameManager.Get().PlayerPickUp();
        Destroy(this.gameObject);
    }

}
