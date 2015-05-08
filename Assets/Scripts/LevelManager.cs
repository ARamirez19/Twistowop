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

	private GameObject GUIObj;

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
		GUIObj = GameObject.FindGameObjectWithTag("GUI");
		GUIObj.transform.FindChild("LevelCompleteGUI").gameObject.SetActive(false);
	}

	void Update ()
	{
		if (state == e_GAMESTATE.PLAYING || state == e_GAMESTATE.PAUSED)
		{
			currentTimer += Time.deltaTime;
		}

		if (state == e_GAMESTATE.MENU)
		{
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
			{
				gsManager.SetGameState(e_GAMESTATE.PLAYING);
			}
		}

	}

	public void ChangeState(e_GAMESTATE e_state)
	{
		if (state == e_GAMESTATE.PLAYING && e_state == e_GAMESTATE.LEVELCOMPLETE)
		{
			GUIObj.transform.FindChild("LevelCompleteGUI").gameObject.SetActive(true);
			
			GUIObj.transform.FindChild("LevelCompleteGUI").FindChild("CompleteText").GetComponent<Text>().text =
				"Your time: " + System.Math.Round (currentTimer,2) + "s\nTime to Beat: " +
					System.Math.Round (timeToBeat,2) + "s\nTimes Frozen: " + timesFrozen;
		}

		if (state == e_GAMESTATE.MENU && e_state == e_GAMESTATE.PLAYING)
		{
			GUIObj.transform.FindChild("MenuGUI").gameObject.SetActive(false);
		}

		state = e_state;
	}

	public void NextLevel()
	{
		if (Application.loadedLevel == Application.levelCount -1)
			Application.LoadLevel(0);
		else
			Application.LoadLevel(Application.loadedLevel + 1);
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