using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockonEnemy : EnemyController
{
	public GameObject player;
	[SerializeField]
	private float movementSpeed;
	[SerializeField]
	private float distance;
	[SerializeField]
	private float lockonTimer;
	private Vector2 enemyDirection;
	private bool lockingOn = false;
	
	// Start is called before the first frame update
	protected override void ExtraStart()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update()
	{
		if(state == GameState.e_GAMESTATE.PLAYING && lockingOn == false)
		{
			enemyDirection = player.transform.position - transform.position;
			enemyDirection = enemyDirection.normalized;
			transform.Translate(enemyDirection * movementSpeed * Time.deltaTime);
		}
		
		if(Vector2.Distance(this.gameObject.transform.position, player.transform.position) < distance)
		{
			lockingOn = true;
			StartCoroutine(LockonTimer());
		}
	}
	
	IEnumerator LockonTimer()
	{
		yield return new WaitForSeconds(lockonTimer);
		if(Vector2.Distance(this.gameObject.transform.position, player.transform.position) < distance)
		{
			levelManager.RestartLevel();
		}
		else
		{
			lockingOn = false;
		}
	}
}