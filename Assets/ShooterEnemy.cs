using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameState;

public class ShooterEnemy : MonoBehaviour, IGameState
{
    private GameStateManager gsManager;
    private e_GAMESTATE state;
    [SerializeField]
    private GameObject bullet;
	[SerializeField]
	private GameObject canonPosition;
    [SerializeField]
    private float interval = 2;
    [SerializeField]
    private int speed = 2;
    private float timer;

    private void Start()
    {
        gsManager = GameStateManager.GetInstance();
        gsManager.GameStateSubscribe(this.gameObject);
        state = gsManager.GetGameState();
        timer = interval;
        bullet.GetComponent<Bullet>().SetSpeed(speed);
		canonPosition = GameObject.Find("Shooter_Canon");
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.e_GAMESTATE.PLAYING)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Instantiate(bullet, canonPosition.transform.position + this.transform.up * (this.GetComponent<Renderer>().bounds.size.y / 2), transform.rotation);
                timer = interval;
            }
        }
    }

    public void ChangeState(e_GAMESTATE m_state)
    {
        state = m_state;
    }
}
