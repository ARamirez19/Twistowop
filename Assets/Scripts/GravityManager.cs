using UnityEngine;
using System.Collections;

public class GravityManager : MonoBehaviour
{
	private const float GRAV_SPEED = 9.81f;

	void Start ()
	{
		Screen.orientation = ScreenOrientation.Landscape;
	}
	
	// Update is called once per frame
	void Update ()
	{
		CalcGrav();
	}

	void CalcGrav()
	{
		float xx;
		float yy;
		/*
		if (!Application.isMobilePlatform)
		{
			xx = Mathf.Sin (-Camera.main.transform.localEulerAngles.z*Mathf.Deg2Rad) * GRAV_SPEED;
			yy = Mathf.Cos (-Camera.main.transform.localEulerAngles.z*Mathf.Deg2Rad) * GRAV_SPEED;
			Physics.gravity = new Vector3(xx,yy,0f);
		}
		else 
		{
		*/

		xx = Mathf.Sin (-Input.acceleration.z * Mathf.Deg2Rad) * GRAV_SPEED;
		yy = Mathf.Cos (-Input.acceleration.z * Mathf.Deg2Rad) * GRAV_SPEED;

		//Physics.gravity = new Vector3 (Input.acceleration.x *GRAV_SPEED, Input.acceleration.y * GRAV_SPEED, 0f);
		//}
		//Try Cosecant of that weird fucking angle you just tried to use (Input.acceleration.z to see if we can use that to get the right stuff from it, get it? heheheh)

		//Debug.Log ("(" + Input.gyro.rotationRate.x + ", " + Input.gyro.rotationRate.y + ", " + Input.gyro.rotationRate.z + ")");

		//Physics.gravity = new Vector3(Input.gyro.gravity.x * GRAV_SPEED, Input.gyro.gravity.y * GRAV_SPEED, 0f);

		
			
		//Camera.main.transform.rotation = Quaternion.Euler(0f,0f,-rotateField);

	}
}
