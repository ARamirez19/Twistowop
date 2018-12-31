using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField]
    private bool affectedByGravity;
    private Rigidbody body;
    [SerializeField]
    private float springForce = 100.0f;
    [SerializeField]
    private float springVelocity = 5.0f;
    [SerializeField]
    private bool useVelocity;
    [SerializeField]
    private bool useDirectionalHit;
    private Vector3 direction;  
	// Use this for initialization
	void Start ()
    {
        body = this.GetComponent<Rigidbody>();
	    if(!affectedByGravity)
        {
            body.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            body.constraints = RigidbodyConstraints.None;
            body.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }
	}

    private void OnCollisionEnter(Collision other)
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
                other.rigidbody.AddForce(direction * springForce, ForceMode.Impulse);
            }
        }
    }
}
