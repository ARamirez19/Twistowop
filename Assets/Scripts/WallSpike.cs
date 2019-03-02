using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpike : BaseController
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player" && (state == GameState.e_GAMESTATE.PLAYING || state == GameState.e_GAMESTATE.PAUSED))
        {
            levelManager.RestartLevel();
        }
    }
}
