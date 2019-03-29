using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
   
    public void PlayAudio(string audioID)
    {
        AkSoundEngine.PostEvent(audioID, gameObject);
    }

    public void SetState(string stateGroup, string stateID)
    {
        AkSoundEngine.SetState(stateGroup, stateID);
    }
   
}

