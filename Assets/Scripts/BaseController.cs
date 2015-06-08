using UnityEngine;
using System.Collections;
using GameState;

public class BaseController : MonoBehaviour, IGameState
{
	protected GameStateManager gsManager;
	protected e_GAMESTATE state;
	[SerializeField] protected bool doesVelocityStop = true;
	protected Vector3 prevVelocity = Vector3.zero;
	protected Rigidbody rb;
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

		if (GetComponent<Rigidbody>() != null)
			rb = GetComponent<Rigidbody>();
	}
	
	public void ChangeState(e_GAMESTATE e_State)
	{
		if ((state == e_GAMESTATE.PAUSED && e_State == e_GAMESTATE.PLAYING) && isAffectedByFreeze)
		{
			rb.velocity = prevVelocity;
			rb.useGravity = true;
			rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX |RigidbodyConstraints.FreezeRotationY;
		}
		else if ((state == e_GAMESTATE.PLAYING && e_State == e_GAMESTATE.PAUSED) && isAffectedByFreeze)
		{
			if (!doesVelocityStop)
				prevVelocity = rb.velocity;

			rb.velocity = Vector3.zero;
			rb.useGravity = false;
			rb.constraints = RigidbodyConstraints.FreezeAll;
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
