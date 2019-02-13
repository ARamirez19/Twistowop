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
    [SerializeField]
    private float jetpackSpeed = 10.0f;
    [SerializeField]
    private float maxJetpackSpeed = 30.0f;
    [SerializeField]
    private float rotationSpeed = 1.0f;


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

    private void FixedUpdate()
    {
        if(state == e_GAMESTATE.PLAYING)
        {
            float angle = Mathf.Atan2(Physics2D.gravity.x, -Physics2D.gravity.y) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.time * rotationSpeed/1000);
        }
    }

    private void Inputs()
	{
#if UNITY_EDITOR

		if(Input.GetKey(KeyCode.Space))
		{
			if(state == e_GAMESTATE.PLAYING || state == e_GAMESTATE.PAUSED)
			{
                //levelManager.ToggleLevelFreeze();
                UseJetpack();
			}
		}

#else

		if (Input.touchCount > 0 /*&& Input.GetTouch(0).phase == TouchPhase.Began*/)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			RaycastHit hit;

			if (state == e_GAMESTATE.PLAYING || state == e_GAMESTATE.PAUSED)
			{
				if(Physics.Raycast(ray,out hit) && hit.collider.tag == "PausePlay")
				{
					//levelManager.ToggleLevelFreeze();
                    UseJetpack();
				}
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

    private void UseJetpack()
    {
        if (this.GetComponent<Rigidbody2D>().velocity.magnitude < maxJetpackSpeed)
        {
            this.GetComponent<Rigidbody2D>().AddForce(this.transform.up * jetpackSpeed * 10.0f, ForceMode2D.Impulse);
        }
    }
}
