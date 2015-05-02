using UnityEngine;
using System.Collections;
using GameState;

public class LevelManager : MonoBehaviour, IGameState
{
	private GameStateManager gsManager;
	[SerializeField] private float timeToBeat = 0.0f;
	[SerializeField] private int freezeLimit = 0;
	private float currentTimer = 0.0f;

	void Start ()
	{
	
	}

	void Update ()
	{
		
	}

	public void ChangeState(e_GAMESTATE e_state)
	{

	}
}