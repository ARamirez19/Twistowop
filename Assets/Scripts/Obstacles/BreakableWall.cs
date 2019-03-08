using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] private float minBreakSpeed = 5.0f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.relativeVelocity.magnitude >= minBreakSpeed)
        {
            Destroy(this.gameObject);
        }
    }
}
