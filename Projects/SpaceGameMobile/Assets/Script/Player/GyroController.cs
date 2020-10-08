using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GyroController : MonoBehaviour
{
	public Rigidbody rBody;

	public GameObject shipObject;
	public FixedJoystick fJoyStick;
	public FixedJoystick rJoyStick;

	public float pitch; // X axis
	public float yaw;   // Y axis
	public float roll;   // Z axis
	public int rotation = 50;
	public int yawSens = 50;
	public int speedMultiplier = 50;

	public Quaternion defaultRot;
	public float smoothSpeed = 10;

	void OnEnable()
	{
		defaultRot = gameObject.transform.rotation;
		shipObject = gameObject;
		rBody = gameObject.GetComponent<Rigidbody>();
	}

	void Update()
	{
		MovePlayerShip();
	}

	public void MovePlayerShip()
	{
		float z = rJoyStick.inputY * Time.deltaTime * speedMultiplier;
		float y = fJoyStick.inputY * Time.deltaTime * yawSens;
		float x = fJoyStick.inputX * Time.deltaTime * rotation;
		yaw = fJoyStick.inputY;
		pitch = fJoyStick.inputX;
		if (x > 1)
			x = 1;
		if (x < -1)
			x = -1;
		if (y > 1)
			y = 1;
		if (y < -1)
			y = -1;
		if (z > 1)
			z = 1;
		if (z < -1)
			z = -1;
		transform.Rotate(y, x, 0);
		rBody.velocity = shipObject.transform.forward * z * 9;
	}
}
