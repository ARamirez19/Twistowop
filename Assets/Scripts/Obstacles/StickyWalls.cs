using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyWalls : MonoBehaviour
{
	[SerializeField]
	private bool objectStuck = false;
	[SerializeField]
	private bool eligibleStick = true;
	private Rigidbody2D otherRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

	void OnCollisionEnter2D(Collision2D other)
	{
		otherRigidbody = other.gameObject.GetComponent<Rigidbody2D>();
		if(objectStuck == false && eligibleStick == true)
		{
			objectStuck = true;
			otherRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
			StartCoroutine(StuckTimer(otherRigidbody));
		}
	}

	IEnumerator StuckTimer(Rigidbody2D otherRigidbody)
	{
		yield return new WaitForSeconds(2.0f);
		otherRigidbody.constraints = RigidbodyConstraints2D.None;
		objectStuck = false;
		eligibleStick = false;
		yield return new WaitForSeconds(2);
		eligibleStick = true;
	}
}
