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
    private Rigidbody2D rbody;

    void Start()
    {
        gsManager = GameStateManager.GetInstance();
        gsManager.GameStateSubscribe(this.gameObject);
        state = gsManager.GetGameState();
        levelManager = LevelManager.GetInstance();
        rbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.e_GAMESTATE.PLAYING)
        {
            rbody.AddForce(transform.up * speed);
            if (rbody.velocity.magnitude > 1)
            {
                float angle = Mathf.Atan2(rbody.velocity.x, rbody.velocity.y) * Mathf.Rad2Deg;
                this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(-angle, Vector3.forward), 1);
            }
        }
    }

    void FixedUpdate()
    {
        if (state == GameState.e_GAMESTATE.PLAYING)
        {
            
            //this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rbody.velocity.normalized), 1);
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
