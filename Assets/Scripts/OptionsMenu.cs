using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject[] musicVolume;
    [SerializeField]
    private GameObject[] effectVolume;

    public int currentMusicVol = 12;
    public int currenteffectVol = 12;

    public void LowerMusic()
    {
        if(currentMusicVol != 0)
        {
            musicVolume[currentMusicVol-1].SetActive(false);
            currentMusicVol--;
        }
    }

    public void IncreaseMusic()
    {
        if (currentMusicVol != 12)
        {
            musicVolume[currentMusicVol].SetActive(true);
            currentMusicVol++;
        }
    }

    public void LowerSoundEffects()
    {
        if(currenteffectVol != 0)
        {
            effectVolume[currenteffectVol - 1].SetActive(false);
            currenteffectVol--;
        }
    }

    public void IncreaseSoundEffects()
    {
        if (currenteffectVol != 12)
        {
            effectVolume[currenteffectVol].SetActive(true);
            currenteffectVol++;
        }
    }
}
