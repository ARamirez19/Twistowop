using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GameState;

public class LevelManager : MonoBehaviour, IGameState
{
	private static LevelManager levelManager;
	[SerializeField] private float timeToBeat = 0.0f;
	private float currentTimer = 0.0f;

	[SerializeField] private int freezeLimit = 0;
	private int timesFrozen = 0;
	[SerializeField] private bool canPlayerFreeze = true;

	private GameStateManager gsManager;
	private e_GAMESTATE state;

	private GameObject levelCompGUI;

	public static LevelManager GetInstance()
	{
		if (levelManager == null)
		{
			GameObject go = GameObject.FindGameObjectWithTag ("LevelManager");
			
			if (go == null)
			{
				go = new GameObject();
				go.name = "LevelManager";
				go.tag = "LevelManager";
				levelManager = go.AddComponent<LevelManager>();
			}
			else if (go.GetComponent<LevelManager>() == null)
				levelManager = go.AddComponent<LevelManager>();
			else
				levelManager = go.GetComponent<LevelManager>();
		}

		return levelManager;
	}

	void Start ()
	{
		levelManager = LevelManager.GetInstance();
		gsManager = GameStateManager.GetInstance();
		state = gsManager.GetGameState();
		gsManager.GameStateSubscribe(this.gameObject);
		levelCompGUI = GameObject.FindGameObjectWithTag("LevelCompleteGUI");
		levelCompGUI.SetActive(false);
	}

	void Update ()
	{
		if (state == e_GAMESTATE.PLAYING || state == e_GAMESTATE.PAUSED)
		{
			currentTimer += Time.deltaTime;
		}

		if (state == e_GAMESTATE.LEVELCOMPLETE)
		{
			if (levelCompGUI.activeSelf == false)
			{
				levelCompGUI.SetActive(true);

				levelCompGUI.transform.FindChild("CompleteText").GetComponent<Text>().text =
					"Your time: " + System.Math.Round (currentTimer,2) + "s\nTime to Beat: " +
						System.Math.Round (timeToBeat,2) + "s\nTimes Frozen: " + timesFrozen;
			}
		}
	}

	public void ChangeState(e_GAMESTATE e_state)
	{
		state = e_state;
	}

	public void RestartLevel()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	public bool GetPlayerFreezeStatus()
	{
		return canPlayerFreeze;
	}

	public void ToggleLevelFreeze()
	{
		if (state == e_GAMESTATE.PLAYING)
		{
			if (timesFrozen < freezeLimit || freezeLimit < 0)
			{
				gsManager.SetGameState(e_GAMESTATE.PAUSED);
				timesFrozen++;
			}
		}
		else if (state == e_GAMESTATE.PAUSED)
			gsManager.SetGameState(e_GAMESTATE.PLAYING);
	}
}