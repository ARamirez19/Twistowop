using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerEnemy : EnemyController
{
	private float speed = 5.0f;
	private float startTime;
	private float journeyLength;
	private float distCovered;
	private float fracJourney;

	private float returnTime;
	private float returnLength;
	private float returnDist;
	private float returnFrac;

	[SerializeField]
	private bool moveToEndPos = false;
	[SerializeField]
	private bool returnToStartPos = false;

	[SerializeField]
	private GameObject field;
	[SerializeField]
	private Transform startPos;
	[SerializeField]
	private Transform endPos;
	[SerializeField]
	private GameObject movementSprite;

    // Start is called before the first frame update
    protected override void ExtraStart()
    {
		field = this.gameObject.transform.GetChild(0).gameObject;
		movementSprite = this.gameObject.transform.GetChild(1).gameObject;
		startPos = this.gameObject.transform.GetChild(2).transform;
		endPos = this.gameObject.transform.GetChild(3).transform;
		field.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
		if(moveToEndPos == true)
		{
			distCovered = (Time.time - startTime) * speed;
			fracJourney = distCovered / journeyLength;
			movementSprite.transform.position = Vector2.Lerp(startPos.position, endPos.position, fracJourney);
		}
		if(returnToStartPos == true)
		{
			returnDist = (Time.time - returnTime) * speed;
			returnFrac = returnDist / returnLength;
			movementSprite.transform.position = Vector2.Lerp(endPos.position, startPos.position, returnFrac);
		}
		if(movementSprite.transform.position == endPos.transform.position)
		{
			moveToEndPos = false;
			ReturnCharger();
		}

		if(Input.GetKeyDown (KeyCode.Space))
		{
			MoveCharger();
		}

		if(movementSprite.transform.position.x == endPos.transform.position.x)
		{
			ReturnCharger();
		}
		if(movementSprite.transform.position.x == startPos.transform.position.x)
		{
			returnToStartPos = false;
		}
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player" && moveToEndPos == false && returnToStartPos == false)
		{
			MoveCharger();
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