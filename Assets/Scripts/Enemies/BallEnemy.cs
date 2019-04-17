using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEnemy : EnemyController
{
    private GameObject player;
    private float waitTime = 2.0f;

    protected override void ExtraStart()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player" && (state == GameState.e_GAMESTATE.PLAYING || state == GameState.e_GAMESTATE.PAUSED))
        {
            base.deathAnimation = true;
            StartCoroutine(DeathTimer(waitTime));
            player.GetComponent<PlayerController>().deadPlayer = true;
            player.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
