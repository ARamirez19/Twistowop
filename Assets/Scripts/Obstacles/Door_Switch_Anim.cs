using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Switch_Anim : MonoBehaviour
{
    private GameObject door;
    private bool switchEnabled = false;
    private Animator doorAnim;
    private Animator switchAnim;

    public float doorOpenPause = 5.0f;

    void Start()
    {
        door = GameObject.FindGameObjectWithTag("Door");
        doorAnim = door.GetComponent<Animator>();
        switchAnim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player" && switchEnabled == false)
        {
            StartCoroutine(DoorTimer());
        }
    }

    IEnumerator DoorTimer()
    {
        switchEnabled = true;
        doorAnim.SetBool("activated", true);
        switchAnim.SetBool("activated", true);

        yield return new WaitForSeconds(doorOpenPause);
        switchEnabled = false;
        doorAnim.SetBool("activated", false);
        switchAnim.SetBool("activated", false);

    }
}
