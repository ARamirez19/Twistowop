using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotTouch : EnemyController
{
    private Animator myAnim;
    private float waitTime;
    private bool movePlayer = false;
    [SerializeField]
    private float thrust = 15.5f;
    private GameObject player;
    private Vector2 direction;

    protected override void ExtraStart()
    {
        myAnim = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (movePlayer == true)
        {
            player.GetComponent<Rigidbody2D>().velocity += direction * thrust;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player" && (state == GameState.e_GAMESTATE.PLAYING || state == GameState.e_GAMESTATE.PAUSED))
        {
            direction = (other.transform.position - this.transform.position).normalized;
            myAnim.SetTrigger("myTrigger");
            waitTime = 2.0f;
            base.deathAnimation = true;
            StartCoroutine(PushPlayer());
            player.GetComponent<PlayerController>().deadPlayer = true;
            base.StartCoroutine(DeathTimer(waitTime));
            player.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private IEnumerator PushPlayer()
    {
        movePlayer = true;
        yield return new WaitForSeconds(0.1f);
        movePlayer = false;
    }
}