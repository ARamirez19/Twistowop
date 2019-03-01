using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
	private GameObject door;
	private Renderer doorRenderer;
	private Collider2D doorCollider;
	private bool switchEnabled = false;
    // Start is called before the first frame update
    void Start()
    {
		door = GameObject.FindGameObjectWithTag("Door");
		doorRenderer = door.GetComponent<Renderer>();
		doorCollider = door.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.collider.tag == "Player" && switchEnabled == false)
		{
			StartCoroutine(DoorTimer());
		}
	}

	IEnumerator DoorTimer()
	{
		switchEnabled = true;
		doorRenderer.enabled = false;
		doorCollider.enabled = false;
		yield return new WaitForSeconds(5);
		switchEnabled = false;
		doorRenderer.enabled = true;
		doorCollider.enabled = true;
	}
}