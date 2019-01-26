using UnityEngine;
using System.Collections;

public class EnemyController : BaseController
{

	protected override void ExtraStart ()
	{

	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.tag == "Player" && (state == GameState.e_GAMESTATE.PLAYING || state == GameState.e_GAMESTATE.PAUSED))
		{
			levelManager.RestartLevel();
		}
	}
}
