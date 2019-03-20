using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubark_Anim : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator myAnim;
    private PlayerController pcScript;

    public float rotVelocity;
    public float velocity;
    public bool jetPack;

    void Start()
    {
        rb2d = GetComponentInParent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        pcScript = GetComponentInParent<PlayerController>();
    }

    void FixedUpdate()
    {
        rotVelocity = rb2d.angularVelocity;
        velocity = rb2d.velocity.magnitude;
        //jetPack = pcScript.jetpackEnabled;

        myAnim.SetFloat("TurnSpeed", rotVelocity);
        myAnim.SetFloat("MoveSpeed", velocity);

        if (jetPack)
        {
            myAnim.SetTrigger("Boost");
            jetPack = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        myAnim.SetTrigger("Hit");
    }
}
