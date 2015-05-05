using UnityEngine;
using System.Collections;
using GameState;

public class PlayerController : BaseController
{
	private bool playerInGoal = false;
	private bool canFreezeLevel = true;
	private float completionTimer = 0.1f;
	private float timer = 0.0f;
	private bool playerInputEnabled = false;

	void Update()
	{
		if ((state == e_GAMESTATE.PLAYING || state == e_GAMESTATE.PAUSED) && canFreezeLevel && playerInputEnabled)
			Inputs();

		if (state == e_GAMESTATE.PLAYING)
		{
			if (playerInputEnabled == false)
				playerInputEnabled = true;

			if (playerInGoal)
			{
				if (rb.velocity.magnitude < .05f)
					timer += Time.deltaTime;
				else
					timer = 0.0f;

				if (timer >= completionTimer)
					gsManager.SetGameState(e_GAMESTATE.LEVELCOMPLETE);
			}
		}
	}

	private void Inputs()
	{
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			RaycastHit hit;

			if (state == e_GAMESTATE.PLAYING || state == e_GAMESTATE.PAUSED)
			{
				if (Physics.Raycast(ray,out hit) && hit.collider.tag == "PausePlay")
				{
					levelManager.ToggleLevelFreeze();
				}
			}


		}
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Goal")
		{
			playerInGoal = true;
			Debug.Log ("Player in goal!");
		}
	}

	public void OnTriggerExit(Collider other)
	{
		if (other.tag == "Goal")
		{
			playerInGoal = false;
			Debug.Log ("Player out of goal!");
		}
	}

	protected override void ExtraStart ()
	{
		canFreezeLevel = levelManager.GetPlayerFreezeStatus();
	}
}
