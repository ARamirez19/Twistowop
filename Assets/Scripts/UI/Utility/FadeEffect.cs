using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    [SerializeField] private bool playOnEnable = false;
    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOut = false;
    [SerializeField] private float fadeInSpeed = 0f;
    [SerializeField] private float fadeOutSpeed = 0f;
    [Space]
    [SerializeField] private CanvasGroup group;

    public Action onFadeInComplete;
    public Action onFadeOutComplete;

    private float alphaOffset = 0.01f;

    private void OnEnable()
    {
        if (playOnEnable)
        {
            if (fadeIn)
            {
                Fade();
            }
            else if (fadeOut)
            {
                Fade(true);
            }
        }
    }

    public void FadeIn()
    {
        if (fadeIn)
        {
            Fade();
        }
    }

    public void FadeOut()
    {
        if (fadeOut)
        {
            Fade(true);
        }
    }

    private void Fade(bool fadingOut = false)
    {
        float alpha = fadingOut ? 0 : 1;
        float fadeSpeed = fadingOut ? fadeOutSpeed : fadeInSpeed;
        StartCoroutine(FadeRoutine(alpha, fadeSpeed, fadingOut));
    }

    private IEnumerator FadeRoutine(float alpha, float fadeSpeed, bool fadingOut)
    {
        while (group.alpha < alpha - alphaOffset || group.alpha > alpha + alphaOffset)
        {
            group.alpha = Mathf.Lerp(group.alpha, alpha, fadeSpeed / 10);
            yield return null;
        }
        group.alpha = alpha;

        if (fadingOut)
        {
            if (onFadeOutComplete != null)
            {
                onFadeOutComplete();
            }
        }
        else
        {
            if (onFadeInComplete != null)
            {
                onFadeInComplete();
            }
        }
    }
}
