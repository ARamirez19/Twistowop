using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameState;

public class Bullet : MonoBehaviour, IGameState
{
    private GameStateManager gsManager;
    private e_GAMESTATE state;
    [SerializeField]
    private int speed;
    protected LevelManager levelManager;

    void Start()
    {
        gsManager = GameStateManager.GetInstance();
        gsManager.GameStateSubscribe(this.gameObject);
        state = gsManager.GetGameState();
        levelManager = LevelManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.e_GAMESTATE.PLAYING)
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed);
        }
    }

    public void SetSpeed(int s)
    {
        speed = s;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player" && (state == GameState.e_GAMESTATE.PLAYING))
        {
            levelManager.RestartLevel();
        }
        else
        {
            gsManager.GameStateUnSubscribe(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void ChangeState(e_GAMESTATE m_state)
    {
        state = m_state;
    }
}
