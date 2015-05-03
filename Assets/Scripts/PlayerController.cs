using UnityEngine;
using System.Collections;
using GameState;

public class PlayerController : BaseController
{
	private bool playerInGoal = false;
	private float freezeCount = 0;
	private float completionTimer = 1.0f;
	private float timer = 0.0f;

	void Update()
	{
		if (state == e_GAMESTATE.PLAYING || state == e_GAMESTATE.PAUSED)
			Inputs();

		if (state == e_GAMESTATE.PLAYING)
		{
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

			switch(state)
			{
			case e_GAMESTATE.PLAYING:
				if (Physics.Raycast(ray,out hit) && hit.collider.tag == "PausePlay")
				{
					gsManager.SetGameState(e_GAMESTATE.PAUSED);
					freezeCount++;
				}
				break;
			case e_GAMESTATE.PAUSED:
				if (Physics.Raycast(ray,out hit) && hit.collider.tag == "PausePlay")
					gsManager.SetGameState(e_GAMESTATE.PLAYING);
				break;
			case e_GAMESTATE.LEVELCOMPLETE:
				//Do Level Complete stuff like... Continue to the next screen or something. Or maybe GUIManager can do that.
				break;
			default:
				break;
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
}
