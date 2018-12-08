using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarEffect : MonoBehaviour
{
    [SerializeField] private LevelCompleteController controller;

    public void FinishedAnimation()
    {
        controller.NextStar();
    }
}
