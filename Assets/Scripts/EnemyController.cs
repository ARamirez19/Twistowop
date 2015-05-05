using UnityEngine;
using System.Collections;

public class EnemyController : BaseController
{
	

	void Update ()
	{
	
	}

	protected override void ExtraStart ()
	{

	}

	void OnCollisionEnter(Collision other)
	{
		if (other.collider.tag == "Player" && (state == GameState.e_GAMESTATE.PLAYING || state == GameState.e_GAMESTATE.PAUSED))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
