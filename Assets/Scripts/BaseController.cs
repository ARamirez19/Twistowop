using UnityEngine;
using System.Collections;
using GameState;

public class BaseController : MonoBehaviour, IGameState
{
	protected GameStateManager gsManager;
	protected e_GAMESTATE state;
	[SerializeField] protected bool doesVelocityStop = true;
	protected Vector3 prevVelocity = Vector3.zero;
	protected Rigidbody2D rb;
	protected LevelManager levelManager;
	[SerializeField] protected bool isAffectedByFreeze = true;
	[SerializeField] protected bool isAffectedByGravityWells = false;
	
	void Start()
	{
		gsManager = GameStateManager.GetInstance();
		gsManager.GameStateSubscribe(this.gameObject);
		state = gsManager.GetGameState();

		levelManager = LevelManager.GetInstance();

		ExtraStart();

		if (GetComponent<Rigidbody2D>() != null)
			rb = GetComponent<Rigidbody2D>();
	}
	
	public void ChangeState(e_GAMESTATE e_State)
	{
		if ((state == e_GAMESTATE.PAUSED && e_State == e_GAMESTATE.PLAYING) && isAffectedByFreeze)
		{
            rb.gravityScale = 1.0f;
            rb.velocity = prevVelocity;
            rb.constraints = RigidbodyConstraints2D.None;
			
		}
		else if ((state == e_GAMESTATE.PLAYING && e_State == e_GAMESTATE.PAUSED) && isAffectedByFreeze)
		{
			if (!doesVelocityStop)
				prevVelocity = rb.velocity;

			rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
		}
        else if((state == e_GAMESTATE.MENU && e_State == e_GAMESTATE.PLAYING) && isAffectedByFreeze)
        {
            rb.gravityScale = 1f;
        }
		state = e_State;
	}

	void OnDestroy()
	{
		gsManager.GameStateUnSubscribe(this.gameObject);
	}

	protected virtual void ExtraStart(){}

	public bool GravWellStatus()
	{
		return isAffectedByGravityWells;
	}
}
