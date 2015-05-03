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

	void Start ()
	{
		state = gsManager.GetGameState();
		gsManager = GameStateManager.GetInstance();
		gsManager.GameStateSubscribe(this.gameObject);
	}

	void Update ()
	{
		if (state == e_GAMESTATE.PLAYING)
		{
			currentTimer += Time.deltaTime;
		}
		if (state == e_GAMESTATE.LEVELCOMPLETE)
		{

		}
	}

	public void ChangeState(e_GAMESTATE e_state)
	{
		state = e_state;
	}
}