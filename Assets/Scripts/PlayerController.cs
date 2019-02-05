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
    /*[SerializeField]*/ float boostSpeed = 25f;

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
                /*
				if (rb.velocity.magnitude < .05f)
					timer += Time.deltaTime;
				else
					timer = 0.0f;

				if (timer >= completionTimer)*/
					gsManager.SetGameState(e_GAMESTATE.LEVELCOMPLETE);
			}
		}
	}

	private void Inputs()
	{
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.Space))
		{
			if(state == e_GAMESTATE.PLAYING || state == e_GAMESTATE.PAUSED)
			{
                JumpBoost();
				//levelManager.ToggleLevelFreeze();
			}
        }
#else

		if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Began))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			RaycastHit hit;

            JumpBoost();
   //         else if (state == e_GAMESTATE.PLAYING || state == e_GAMESTATE.PAUSED)
			//{
			//	if (Physics.Raycast(ray,out hit) && hit.collider.tag == "PausePlay")
			//	{
			//		levelManager.ToggleLevelFreeze();
			//	}
			//}
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

    private void JumpBoost()
    {
        Physics2D.gravity = Vector2.zero;
#if UNITY_EDITOR
        rb.velocity = Vector2.Lerp(rb.velocity, Camera.main.transform.up * boostSpeed, 0.3f);
#else
        rb.velocity = Vector2.Lerp(rb.velocity, Input.gyro.gravity * -boostSpeed, 0.3f);
#endif
    }

    private bool DoubleTap()
    {
#if !UNITY_EDITOR

        //if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        //{
        //    float DeltaTime = Input.GetTouch(0).deltaTime;
        //    float DeltaPositionLenght = Input.GetTouch(0).deltaPosition.magnitude;

        //    if (DeltaTime > 0 && DeltaTime < doubleTapTime && DeltaPositionLenght < doubleTapDelta)
        //        return true;
        //}
        //return false;
#endif
        if (Input.GetKey(KeyCode.Space))
        {
            return true;
        }
        return false;
    }
}
