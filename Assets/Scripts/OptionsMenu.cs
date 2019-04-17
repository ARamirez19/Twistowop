using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectsSlider;


    private void Awake()
    {
        musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        effectsSlider = GameObject.Find("EffectsSlider").GetComponent<Slider>();
        float savedMusicVol = PlayerPrefs.GetFloat("MusicVol", musicSlider.maxValue);
        float savedEffectsVol = PlayerPrefs.GetFloat("EffectsVol", effectsSlider.maxValue);
        musicSlider.value = savedMusicVol;
        effectsSlider.value = savedEffectsVol;
        SetMusicVol(savedMusicVol);
        SetEffectsVol(savedEffectsVol);
        musicSlider.onValueChanged.AddListener((float _) => SetMusicVol(_));
        effectsSlider.onValueChanged.AddListener((float _) => SetEffectsVol(_));
    }

    void SetMusicVol(float vol)
    {
        //Add code to change vol
        PlayerPrefs.SetFloat("MusicVol", vol);
    }

    void SetEffectsVol(float vol)
    {
        //Add code to change vol
        PlayerPrefs.SetFloat("EffectsVol", vol);
    }
}
