using UnityEngine;
using System.Collections;
using GameState;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField]
    private int jetpackTimer = 2;
    private float jetpackTime = 2;
    [SerializeField]
    private float jetpackFuel = 10.0f;
    private float maxFuel;
    [SerializeField]
    private Image jetpackBar;
    [SerializeField]
    private Image timerImage;
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private bool jetpackEnabled = false;
    [SerializeField]
    private bool jetpackCooldown = false;
    [SerializeField]
    private bool rotatePlayer = false;


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
        if (!jetpackCooldown)
        {
            if (jetpackFuel < maxFuel)
            {
                jetpackFuel = jetpackFuel + 0.05f;
                jetpackBar.fillAmount = jetpackFuel / maxFuel;
            }
        }
        else
        {
            jetpackTime += Time.deltaTime;
            if(jetpackTime > jetpackTimer)
            {
                jetpackTime = jetpackTimer;
            }
            timerText.text = ((int)jetpackTime).ToString();
        }
	}

    private void FixedUpdate()
    {
        if(state == e_GAMESTATE.PLAYING && rotatePlayer)
        {
            float angle = Mathf.Atan2(Physics2D.gravity.x, -Physics2D.gravity.y) * Mathf.Rad2Deg; //Converts the gravity x and y values into an angle
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.time * rotationSpeed/1000); //Rotates player to that angle
        }
    }

    private void Inputs()
	{
        #if UNITY_EDITOR
        if (!jetpackCooldown)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (state == e_GAMESTATE.PLAYING || state == e_GAMESTATE.PAUSED)
                {
                    //levelManager.ToggleLevelFreeze();
                    if (jetpackEnabled)
                    {
                        UseJetpack();
                    }
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (state == e_GAMESTATE.PLAYING || state == e_GAMESTATE.PAUSED)
                {
                    if(jetpackEnabled)
                    {
                        UseJetpack();
                    }
                }
            }
        }

        #else
        if(!jetpackCooldown)
        {
		if (Input.touchCount > 0)
		    {
			    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			    RaycastHit hit;

			    if (state == e_GAMESTATE.PLAYING || state == e_GAMESTATE.PAUSED)
			    {
				    if(Physics.Raycast(ray,out hit) && hit.collider.tag == "PausePlay" && jetpackEnabled)
				    {
					    //levelManager.ToggleLevelFreeze();
                        UseJetpack();
				    }
			    }
		    }
        }
        else
        {
		    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		    {
			    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			    RaycastHit hit;

			    if (state == e_GAMESTATE.PLAYING || state == e_GAMESTATE.PAUSED)
			    {
				    if(Physics.Raycast(ray,out hit) && hit.collider.tag == "PausePlay" && jetpackEnabled)
				    {
					    //levelManager.ToggleLevelFreeze();
                        UseJetpack();
				    }
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
        jetpackBar = GameObject.Find("JetpackMeter").GetComponent<Image>();
        timerImage = GameObject.Find("JetpackTimer").GetComponent<Image>();
        timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
        jetpackBar.gameObject.SetActive(false);
        timerImage.gameObject.SetActive(false);
        maxFuel = jetpackFuel;
	}

    private void UseJetpack()
    {
        if (!jetpackCooldown)
        {
            if (this.GetComponent<Rigidbody2D>().velocity.magnitude < maxJetpackSpeed && jetpackFuel > 0.0f)
            {
                this.GetComponent<Rigidbody2D>().AddForce(this.transform.up * jetpackSpeed * 10.0f, ForceMode2D.Impulse);
                jetpackFuel = jetpackFuel - 0.1f;
                jetpackBar.fillAmount = jetpackFuel / maxFuel;
            }
        }
        else
        {
            if (jetpackTime >= jetpackTimer)
            {
                this.GetComponent<Rigidbody2D>().AddForce(this.transform.up * jetpackSpeed * 200.0f, ForceMode2D.Impulse);
                jetpackTime = 0;
            }
        }
    }

    public void UseJetpack(bool s)
    {
        jetpackEnabled = s;
        if (jetpackCooldown)
        {
            timerImage.gameObject.SetActive(true);
            jetpackBar.gameObject.SetActive(false);
        }
        else
        {
            jetpackBar.gameObject.SetActive(true);
            timerImage.gameObject.SetActive(false);
        }
        if (!jetpackEnabled)
        {
            timerImage.gameObject.SetActive(false);
            jetpackBar.gameObject.SetActive(false);
        }
    }


    public void UseCooldown(bool s)
    {
        jetpackCooldown = s;
        if (jetpackCooldown)
        {
            timerImage.gameObject.SetActive(true);
            jetpackBar.gameObject.SetActive(false);
        }
        else
        {
            jetpackBar.gameObject.SetActive(true);
            timerImage.gameObject.SetActive(false);
        }
        if (!jetpackEnabled)
        {
            timerImage.gameObject.SetActive(false);
            jetpackBar.gameObject.SetActive(false);
        }
    }


    public void RotatePlayer(bool s)
    {
        rotatePlayer = s;
    }
}
