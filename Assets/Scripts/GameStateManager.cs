using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameState;

namespace GameState
{
	public enum e_GAMESTATE
	{
		PAUSED,
		PLAYING,
		MENU,
		LEVELCOMPLETE
	}
}

public class GameStateManager : MonoBehaviour
{
	public static GameStateManager gsManager;
	[SerializeField] private e_GAMESTATE state = e_GAMESTATE.MENU;
	private List<GameObject> subscribers = new List<GameObject>();

	void Start ()
	{

	}

	void Update ()
	{
	
	}

	public static GameStateManager GetInstance()
	{
		if (gsManager == null)
		{
			GameObject go = GameObject.Find ("GameStateManager");

			if (go == null)
			{
				go = new GameObject();
				go.name = "GameStateManager";
				gsManager = go.AddComponent<GameStateManager>();
			}
			else if (go.GetComponent<GameStateManager>() == null)
			{
				gsManager = go.AddComponent<GameStateManager>();
			}
			else
				gsManager = go.GetComponent<GameStateManager>();
		}

		return gsManager;
	}

	public e_GAMESTATE GetGameState()
	{
		return state;
	}

	public void SetGameState(e_GAMESTATE e_state)
	{
		Debug.Log (e_state);

		state = e_state;

		foreach(GameObject obj in subscribers)
		{
			obj.GetComponent<IGameState>().ChangeState(e_state);
		}
	}

	public void GameStateSubscribe(GameObject go)
	{
		subscribers.Add (go);
	}

	public void GameStateUnSubscribe(GameObject go)
	{
		subscribers.Remove (go);
	}
}
