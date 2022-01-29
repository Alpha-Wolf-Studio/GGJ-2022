using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    [SerializeField] ParticleSystem ownParticleSystem;

    bool currentActivated;

    private void Start()
    {
        GameManager.Get().OnNewCheckpoint += DeactivateCheckpoint;
        GameManager.Get().OnColorChange += InvertColor;
        ownParticleSystem.Stop();
    }

    void DeactivateCheckpoint() 
    {
        currentActivated = false;
        ownParticleSystem.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentActivated) return;

        GameManager.Get().NewPlayerSavedPosition(transform.position);
        currentActivated = true;
        ownParticleSystem.Play();
    }

    void InvertColor(bool yingState) 
    {
        var module = ownParticleSystem.main;
        ParticleSystem.Particle[] aliveParticles;
        aliveParticles = new ParticleSystem.Particle[module.maxParticles];
        int numberOfAliveParticles = ownParticleSystem.GetParticles(aliveParticles);
        if (yingState) 
        {
            module.startColor = ColorGeneral.OniWhite;
            for (int i = 0; i < numberOfAliveParticles; i++)
            {
                aliveParticles[i].startColor = ColorGeneral.OniWhite;
            }
        }
        else 
        {
            
            module.startColor = ColorGeneral.OniBlack;
            for (int i = 0; i < numberOfAliveParticles; i++)
            {
                aliveParticles[i].startColor = ColorGeneral.OniBlack;
            }
        }
        ownParticleSystem.SetParticles(aliveParticles, numberOfAliveParticles);
    }

}
