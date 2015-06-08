using UnityEngine;
using System.Collections;
using GameState;

public class GravityWell : MonoBehaviour, IGameState
{
	private GameStateManager gsManager;
	private e_GAMESTATE state;
	private GameObject player;
	private float scale = 1.0f;
	[SerializeField] float pullMagnification = .1f;
	//Would have to get subscribers of gravity

	void Start ()
	{
		gsManager = GameStateManager.GetInstance();
		gsManager.GameStateSubscribe(this.gameObject);
		state = gsManager.GetGameState();
		player = GameObject.FindGameObjectWithTag("Player");
		scale = this.transform.localScale.x *.75f;
	}

	void Update ()
	{
		if (player.GetComponent<BaseController>().GravWellStatus() == false)
			return;

		if (state == e_GAMESTATE.PLAYING)
		{
			Vector3 heading = this.transform.position;
			heading.z = 0f;
			heading -= player.transform.position;

			float distance = heading.magnitude;

			if (distance < scale)
			{
				Vector3 direction = heading / distance;
				player.GetComponent<Rigidbody>().velocity += direction * pullMagnification;
			}
		}
	}

	public void ChangeState(e_GAMESTATE m_state)
	{
		state = m_state;
	}
}
