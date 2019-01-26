using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (string.Compare(other.tag, "Player") == 0)
        {
            LevelManager.GetInstance().CurrentCollectableCount++;
            Destroy(this.gameObject);
        }
    }
}
