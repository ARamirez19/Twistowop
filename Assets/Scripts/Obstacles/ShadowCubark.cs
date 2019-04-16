using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCubark : BaseController
{
    private bool spinShadow = false;
    private float waitTime = 2.0f;
    private GameObject player;

    void Update()
    {
        if(spinShadow == true)
        {
            transform.Rotate(Vector3.forward * 500.0f * Time.deltaTime);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Goal")
        {
            Destroy(this.gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player" || other.collider.tag == "Enemy")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerController>().PlayerDeath();
            StartCoroutine(DeathTimer(waitTime));
            spinShadow = true;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            
        }
    }

    private IEnumerator DeathTimer(float time)
    {
        yield return new WaitForSeconds(time);
        levelManager.RestartLevel();
    }
}
