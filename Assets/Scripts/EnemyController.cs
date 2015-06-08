using UnityEngine;
using System.Collections;

public class EnemyController : BaseController
{

	protected override void ExtraStart ()
	{

	}

	void OnCollisionEnter(Collision other)
	{
		if (other.collider.tag == "Player" && (state == GameState.e_GAMESTATE.PLAYING || state == GameState.e_GAMESTATE.PAUSED))
		{
			levelManager.RestartLevel();
		}
	}
}
