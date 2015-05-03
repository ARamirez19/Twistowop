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
	
	void Start()
	{
		gsManager = GameStateManager.GetInstance();
		gsManager.GameStateSubscribe(this.gameObject);
		state = gsManager.GetGameState();

		rb = GetComponent<Rigidbody>();
	}
	
	void Update()
	{
		//Vector3 pos = transform.position;
		//pos.y = Vector3.Dot(Input.gyro.gravity, Vector3.up) * movementScale;
		//transform.position = pos;
		
		
	}
	
	public void ChangeState(e_GAMESTATE e_State)
	{
		if (state == e_GAMESTATE.PAUSED && e_State == e_GAMESTATE.PLAYING)
		{
			rb.velocity = prevVelocity;
			rb.useGravity = true;
			rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX |RigidbodyConstraints.FreezeRotationY;
		}
		else if (state == e_GAMESTATE.PLAYING && e_State == e_GAMESTATE.PAUSED)
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
}
