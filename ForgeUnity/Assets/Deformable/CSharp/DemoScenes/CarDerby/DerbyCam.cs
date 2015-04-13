using UnityEngine;
using System.Collections;

public class DerbyCam : MonoBehaviour 
{
	public GameObject Car;

	void Update () 
	{
		// Follow target car
		transform.LookAt(Car.transform);
	}
}
