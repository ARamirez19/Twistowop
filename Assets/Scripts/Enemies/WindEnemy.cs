using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEnemy : EnemyController
{
	[SerializeField]
	private bool pushObject = true;
	private bool playerPushed = false;
	private bool playerPulled = false;
    [SerializeField]
	private float windPower;
	private GameObject player;
	private Rigidbody2D playerBody;
	[SerializeField]
	private GameObject field;
	[SerializeField]
	private Vector2 startPos;
	[SerializeField]
	private Vector2 endPos;
	private Vector2 windDiection;
	private Vector2 pullDirection;
    private Animator myAnim;

    // Start is called before the first frame update
    protected override void ExtraStart()
    {
		player = GameObject.FindGameObjectWithTag("Player");
		playerBody = player.GetComponent<Rigidbody2D>();
		field = this.gameObject.transform.GetChild(1).gameObject;
		startPos = this.gameObject.transform.GetChild(2).transform.position;
		endPos = this.gameObject.transform.GetChild(3).transform.position;
		field.gameObject.GetComponent<Renderer>().enabled = false;
		windDiection = startPos - endPos;
		pullDirection = endPos - startPos;
        myAnim = this.gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		if(playerPushed == true)
		{
			playerBody.velocity -= windDiection * windPower;
            
		}
		if(playerPulled == true)
		{
			playerBody.velocity -= pullDirection * windPower;
		}
        if(pushObject == true)
        {
            myAnim.SetBool("Pushing", true);
        }
        if(pushObject == false)
        {
            myAnim.SetBool("Pushing", false);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player" && (state == GameState.e_GAMESTATE.PLAYING || state == GameState.e_GAMESTATE.PAUSED))
        {
            base.deathAnimation = true;
            float waitTime = 2.0f;
            StartCoroutine(DeathTimer(waitTime));
            player.GetComponent<PlayerController>().deadPlayer = true;
            player.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player" && pushObject == true)
		{
			playerPushed = true;
		}
		if(other.gameObject.tag == "Player" && pushObject != true)
		{
			playerPulled = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			playerPushed = false;
			playerPulled = false;
		}
	}
}
