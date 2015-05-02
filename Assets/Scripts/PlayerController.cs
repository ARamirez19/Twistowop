using UnityEngine;
using System.Collections;
using GameState;

public class PlayerController : BaseController
{
	void Update()
	{

		if (state == e_GAMESTATE.PLAYING || state == e_GAMESTATE.PAUSED)
			Inputs();
	}

	private void Inputs()
	{
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			if (state == e_GAMESTATE.PLAYING)
				gsManager.SetGameState(e_GAMESTATE.PAUSED);
			else if (state == e_GAMESTATE.PAUSED)
				gsManager.SetGameState(e_GAMESTATE.PLAYING);
		}
	}
}
