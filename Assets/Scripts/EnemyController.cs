using UnityEngine;
using System.Collections;

public class EnemyController : BaseController
{
    public bool deathAnimation = false;
    protected override void ExtraStart ()
	{

	}

	protected void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.tag == "Player" && (state == GameState.e_GAMESTATE.PLAYING || state == GameState.e_GAMESTATE.PAUSED) && deathAnimation == false)
		{
            levelManager.RestartLevel();
		}
	}

    public IEnumerator DeathTimer(float time)
    {
        yield return new WaitForSeconds(time);
        levelManager.RestartLevel();
    }
}