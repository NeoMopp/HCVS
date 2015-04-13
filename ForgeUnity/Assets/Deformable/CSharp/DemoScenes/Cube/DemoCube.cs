using UnityEngine;
using System.Collections;

public class DemoCube : MonoBehaviour 
{
	// Pointer to deformable component in the cube
	Deformable deformable;

	void Awake () 
	{
		// Capture deformable component
		deformable = GetComponent<Deformable>();	
	}
	
	void OnMouseDown () 
	{
		// Repair 25% of damage on click
		deformable.Repair(0.25f);	
	}

	void OnGUI ()
	{
		// Shows help
		GUILayout.Label("Click on the cube to repair it");
	}
}
