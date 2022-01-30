using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    [SerializeField] List<ParticleSystem> particleSystems;

    bool currentActivated;

    private void Start()
    {
        GameManager.Get().OnNewCheckpoint += DeactivateCheckpoint;
        GameManager.Get().OnColorChange += InvertColor;
        foreach (var item in particleSystems)
        {
            item.Stop();
        }
    }

    void DeactivateCheckpoint() 
    {
        currentActivated = false;
        foreach (var item in particleSystems)
        {
            item.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentActivated) return;

        GameManager.Get().NewPlayerSavedPosition(transform.position);
        currentActivated = true;
        foreach (var item in particleSystems)
        {
            item.Play();
        }
    }

    void InvertColor(bool yingState) 
    {
        foreach (var item in particleSystems)
        {
            var module = item.main;
            ParticleSystem.Particle[] aliveParticles;
            aliveParticles = new ParticleSystem.Particle[module.maxParticles];
            int numberOfAliveParticles = item.GetParticles(aliveParticles);
            if (yingState)
            {
                module.startColor = Utils.OniWhite;
                for (int i = 0; i < numberOfAliveParticles; i++)
                {
                    aliveParticles[i].startColor = Utils.OniWhite;
                }
            }
            else
            {

                module.startColor = Utils.OniBlack;
                for (int i = 0; i < numberOfAliveParticles; i++)
                {
                    aliveParticles[i].startColor = Utils.OniBlack;
                }
            }
            item.SetParticles(aliveParticles, numberOfAliveParticles);
        }
    }

}
