using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarEffect : MonoBehaviour
{
    [SerializeField] private LevelCompleteController controller;
    [SerializeField] private ParticleSystem starParticles;

    public void FinishedAnimation()
    {
        controller.NextStar();
        starParticles.Play();
    }
}
