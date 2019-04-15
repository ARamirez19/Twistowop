using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerEnemy : EnemyController
{
	[SerializeField]
	private float speed;
	private float startTime;
	private float journeyLength;
	private float distCovered;
	private float fracJourney;

	private float returnTime;
	private float returnLength;
	private float returnDist;
	private float returnFrac;

	[SerializeField]
	private float incrementSpeed;
	[SerializeField]
	private float maxSpeed;
	[SerializeField]
	private float returnSpeed;

	[SerializeField]
	private bool moveToEndPos = false;
	[SerializeField]
	private bool returnToStartPos = false;
	[SerializeField]
	private bool hitWall = false;
    [SerializeField]
    private bool hitPlayer = false;

	[SerializeField]
	private GameObject field;
	[SerializeField]
	private Transform startPos;
	[SerializeField]
	private Transform endPos;
	[SerializeField]
	private GameObject movementSprite;
    private GameObject player;

    private float waitTime;
    [SerializeField]
    private float thrust;
	private Animator myAnim;
	private Vector2 direction;

    // Start is called before the first frame update
    protected override void ExtraStart()
    {
        player = GameObject.FindGameObjectWithTag("Player");
		field = this.gameObject.transform.GetChild(0).gameObject;
		movementSprite = this.gameObject.transform.GetChild(1).gameObject;
		startPos = this.gameObject.transform.GetChild(2).transform;
		endPos = this.gameObject.transform.GetChild(3).transform;
		field.GetComponent<Renderer>().enabled = false;
		myAnim = movementSprite.GetComponent<Animator>();
		direction = (endPos.transform.position - startPos.transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
		if(moveToEndPos == true)
		{
			speed += incrementSpeed;
			distCovered = (Time.time - startTime) * speed;
			fracJourney = distCovered / journeyLength;
			movementSprite.transform.position = Vector2.Lerp(startPos.position, endPos.position, fracJourney);
			myAnim.SetBool("charging", true);
		}
		if(returnToStartPos == true)
		{
			returnDist = (Time.time - returnTime) * returnSpeed;
			returnFrac = returnDist / returnLength;
			movementSprite.transform.position = Vector2.Lerp(endPos.position, startPos.position, returnFrac);
			myAnim.SetBool("charging", false);
			myAnim.SetBool("returning", true);
		}
		if(speed >= maxSpeed)
		{
			speed = maxSpeed;
		}
		if(movementSprite.transform.position == endPos.transform.position)
		{
			if(hitWall == true)
			{
				StartCoroutine(ImpactTimer());
			}
			else
			{
				moveToEndPos = false;
				ReturnCharger();
			}
		}

		if(Input.GetKeyDown (KeyCode.Space))
		{
			MoveCharger();
		}

		if(movementSprite.transform.position.x == endPos.transform.position.x)
		{
			if(hitWall == true)
			{
				StartCoroutine(ImpactTimer());
			}
			else
			{
				moveToEndPos = false;
				ReturnCharger();
			}
		}
		if(movementSprite.transform.position.x == startPos.transform.position.x)
		{
			returnToStartPos = false;
			myAnim.SetBool("returning", false);
			speed = 0;
		}

        if(hitPlayer == true)
        {
            player.GetComponent<Rigidbody2D>().velocity += direction * thrust;
        }
    }

	public IEnumerator ImpactTimer()
	{
		yield return new WaitForSeconds(1.5f);
		hitWall = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player" && moveToEndPos == false && returnToStartPos == false)
		{
			MoveCharger();
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Wall")
		{
			myAnim.SetTrigger("hitWall");
			hitWall = true;
		}
		if(other.gameObject.tag == "Player")
		{
			myAnim.SetTrigger("hitCubark");
            base.deathAnimation = true;
            waitTime = 2.0f;
            hitPlayer = true;
            player.GetComponent<BoxCollider2D>().enabled = false;
            base.StartCoroutine(DeathTimer(waitTime));
		}
	}

	public void MoveCharger()
	{
		startTime = Time.time;
		moveToEndPos = true;
		journeyLength = Vector2.Distance(startPos.position, endPos.position);
	}

	public void ReturnCharger()
	{
		moveToEndPos = false;
		returnTime = Time.time;
		returnLength = Vector2.Distance(endPos.position, startPos.position);
		returnToStartPos = true;
	}
}