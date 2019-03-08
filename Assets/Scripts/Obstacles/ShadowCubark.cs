using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCubark : BaseController
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Goal")
        {
            Destroy(this.gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            levelManager.RestartLevel();
        }
    }
}
