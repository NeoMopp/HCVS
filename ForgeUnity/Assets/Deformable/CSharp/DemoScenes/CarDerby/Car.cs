using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour 
{
	void FixedUpdate () 
	{
		// Apply force and torque by player input
		rigidbody.AddForce(transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * 20f, ForceMode.Impulse);	
		rigidbody.AddTorque(Vector3.up * Input.GetAxis("Horizontal") * Time.deltaTime * 12f, ForceMode.Impulse);
	}
	
	void OnGUI () 
	{
		// Draw gui message
		GUILayout.Label("Arrows to control the car. Click and hold on car to repair damages under mouse cursor.");
	}
}
