using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameState;

public class GravityManager: MonoBehaviour, IGameState
{
	private const float GRAV_SPEED = 20f;
	private GameStateManager gsManager;
	private e_GAMESTATE state;

	void Start ()
	{
		gsManager = GameStateManager.GetInstance();
		gsManager.GameStateSubscribe(this.gameObject);
		state = gsManager.GetGameState();

		Screen.orientation = ScreenOrientation.Landscape;
		Input.gyro.enabled = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		CalcGrav();
	}

	void CalcGrav()
	{

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


		if (state == e_GAMESTATE.PLAYING)
		{
			float xx;
			float yy;
			float zz;

			xx = Input.gyro.gravity.x;
			yy = Input.gyro.gravity.y;
			zz = Input.gyro.gravity.z;
			
			if (xx > -.2f && xx < .2f  && yy > -.2f && yy < .2f && (zz < -.75f || zz > .75f))
			{
				Physics.gravity = new Vector3(0.0f,-1.0f,0.0f) * GRAV_SPEED;
			}
			else
			{
				xx = Input.gyro.gravity.x * GRAV_SPEED;
				yy = Input.gyro.gravity.y * GRAV_SPEED;
				
				Physics.gravity = new Vector3(xx, yy);
			}
		}
		else if (state == e_GAMESTATE.MENU)
		{
			//Do Menu Stuff
		}
		else if (state == e_GAMESTATE.PAUSED)
		{

		}


		/*Quaternion referenceRotation = Quaternion.identity;
		Quaternion deviceRotation = DeviceRotation.Get();
		Quaternion eliminationOfXY = Quaternion.Inverse(
			Quaternion.FromToRotation(referenceRotation * Vector3.forward, 
		                          deviceRotation * Vector3.forward)
			);
		Quaternion rotationZ = eliminationOfXY * deviceRotation;
		float roll = rotationZ.eulerAngles.z;

		Debug.Log (roll);

		xx = Mathf.Sin (roll * Mathf.Deg2Rad) * GRAV_SPEED;
		yy = Mathf.Cos (roll * Mathf.Deg2Rad) * GRAV_SPEED;

		xx = Mathf.Sin (-Camera.main.transform.localEulerAngles.z * Mathf.Deg2Rad) * GRAV_SPEED;
		yy = Mathf.Cos(-Camera.main.transform.localEulerAngles.z * Mathf.Deg2Rad) * GRAV_SPEED;
*/
		//Physics.gravity = new Vector3(Input.gyro.gravity.x *GRAV_SPEED, Input.gyro.gravity.y *GRAV_SPEED, 0f);



		//Debug.Log (("(" + xx + ", " + yy+ ")"));


		// z is from -1 to 1 (self-multiplying by GRAV_SPEED). Take x / abs(z), y/abs(z)

		//Physics.gravity = new Vector3 (Input.acceleration.x *GRAV_SPEED, Input.acceleration.y * GRAV_SPEED, 0f);
		//}
		//Try Cosecant of that weird fucking angle you just tried to use (Input.acceleration.z to see if we can use that to get the right stuff from it, get it? heheheh)

		//Debug.Log ("(" + Input.gyro.rotationRate.x + ", " + Input.gyro.rotationRate.y + ", " + Input.gyro.rotationRate.z + ")");

		//Physics.gravity = new Vector3(Input.gyro.gravity.x * GRAV_SPEED, Input.gyro.gravity.y * GRAV_SPEED, 0f);

		
			
		//Camera.main.transform.rotation = Quaternion.Euler(0f,0f,-rotateField);

	}

	public void ChangeState(e_GAMESTATE e_state)
	{
		state = e_state;
		//If going from paused to play, enable gravity on all gravity objects
		//If going from play to paused, disable gravity on all gravityObjects and freeze rigidbody.velocity (0,0,0);

		//Some things not affected by pause? oooo.... That can be interesting
	}
}
