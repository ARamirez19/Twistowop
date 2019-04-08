using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTriggerEnter : MonoBehaviour
{

    public Collider2D col2D;
    public Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        col2D = GetComponent<Collider2D>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            myAnim.SetTrigger("myTrigger");
        }
    }
}
