using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollectable : MonoBehaviour
{
    public string audioEventID = "";



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (string.Compare(other.tag, "Player") == 0)
        {
            LevelManager.GetInstance().CurrentCollectableCount++;
            if(audioEventID != "")
                AkSoundEngine.PostEvent(audioEventID, gameObject);
            Destroy(this.gameObject);
        }
    }
}
