using UnityEngine;
using System.Collections;
using GameState;

public class GravityWell : MonoBehaviour, IGameState
{
	private GameStateManager gsManager;
	private e_GAMESTATE state;
	private GameObject player;
    private CircleCollider2D collider;
	[SerializeField] float pullMagnification = .1f;
	//Would have to get subscribers of gravity

	void Start ()
	{
		gsManager = GameStateManager.GetInstance();
		gsManager.GameStateSubscribe(this.gameObject);
		state = gsManager.GetGameState();
		player = GameObject.FindGameObjectWithTag("Player");
        collider = GetComponent<CircleCollider2D>();
	}

	void Update ()
	{
		if (player.GetComponent<BaseController>().GravWellStatus() == false)
			return;

		if (state == e_GAMESTATE.PLAYING)
		{
			Vector2 heading = this.transform.position;
			
			heading -= new Vector2(player.transform.position.x, player.transform.position.y);

			float distance = heading.magnitude;

			if (distance < collider.radius)
			{
				Vector2 direction = heading / distance;
				player.GetComponent<Rigidbody2D>().velocity += direction * pullMagnification;
			}
		}
	}

	public void ChangeState(e_GAMESTATE m_state)
	{
		state = m_state;
	}
}
