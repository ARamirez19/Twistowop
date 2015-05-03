using UnityEngine;
using System.Collections;
using GameState;

public class PlayerController : BaseController
{
	private bool playerInGoal = false;
	private float freezeCount = 0;

	void Update()
	{
		if (state == e_GAMESTATE.PLAYING || state == e_GAMESTATE.PAUSED)
			Inputs();

		if (state == e_GAMESTATE.PLAYING)
		{
			if (playerInGoal && GetComponent<Rigidbody>().velocity == Vector3.zero)
				gsManager.SetGameState(e_GAMESTATE.LEVELCOMPLETE);
		}
	}

	private void Inputs()
	{
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			switch(state)
			{
			case e_GAMESTATE.PLAYING:
				gsManager.SetGameState(e_GAMESTATE.PAUSED);
				freezeCount++;
				break;
			case e_GAMESTATE.PAUSED:
				gsManager.SetGameState(e_GAMESTATE.PLAYING);
				break;
			case e_GAMESTATE.LEVELCOMPLETE:
				//Do Level Complete stuff
				break;
			default:
				break;
			}
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Goal")
			playerInGoal = true;
	}

	public void OnTriggerExit(Collider other)
	{
		if (other.tag == "Goal")
			playerInGoal = false;
	}
}
