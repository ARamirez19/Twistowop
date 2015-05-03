using UnityEngine;
using System.Collections;
using GameState;

public class LevelManager : MonoBehaviour, IGameState
{
	[SerializeField] private float timeToBeat = 0.0f;
	private float currentTimer = 0.0f;

	[SerializeField] private int freezeLimit = 0;

	private GameStateManager gsManager;
	private e_GAMESTATE state;

	private GameObject levelCompGUI;

	void Start ()
	{
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
				levelCompGUI.SetActive(true);
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
}