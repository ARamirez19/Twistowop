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
		#if UNITY_EDITOR

		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(state == e_GAMESTATE.PLAYING || state == e_GAMESTATE.PAUSED)
			{
				levelManager.ToggleLevelFreeze();
			}
		}

#else

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
            

            if (state == e_GAMESTATE.PLAYING || state == e_GAMESTATE.PAUSED)
			{
                /*
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);
                */
                

                Collider2D[] col = Physics2D.OverlapPointAll(Input.GetTouch(0).position);

                if (col.Length > 0)
                {
                    foreach (Collider2D c in col)
                    {
                        if (c.tag == "PausePlay")
                        {
                            levelManager.ToggleLevelFreeze();
                        }
                    }
                }
                /*
                if (hit.collider.tag == "PausePlay")
				{
					levelManager.ToggleLevelFreeze();
				}*/
			}


		}
		#endif
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Goal")
		{
			playerInGoal = true;
			Debug.Log ("Player in goal!");
		}
	}

	public void OnTriggerExit2D(Collider2D other)
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
