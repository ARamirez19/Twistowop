using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField]
    private bool affectedByGravity;
    private Rigidbody2D body;
    [SerializeField]
    private float springForce = 100.0f;
    [SerializeField]
    private float springVelocity = 5.0f;
    [SerializeField]
    private bool useVelocity;
    [SerializeField]
    private bool useDirectionalHit;
    private Vector2 direction;  
	// Use this for initialization
	void Start ()
    {
        body = this.GetComponent<Rigidbody2D>();
	    if(!affectedByGravity)
        {
            body.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            body.constraints = RigidbodyConstraints2D.None;
        }
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            if (useDirectionalHit)
            {
                direction = (other.transform.position - this.transform.position).normalized;
            }
            else
            {
                direction = this.transform.up;
            }
            if (useVelocity)
            {
                other.rigidbody.velocity = direction * springVelocity;
            }
            else
            {
                other.rigidbody.AddForce(direction * springForce * 1000, ForceMode2D.Impulse);
            }
        }
    }
}
