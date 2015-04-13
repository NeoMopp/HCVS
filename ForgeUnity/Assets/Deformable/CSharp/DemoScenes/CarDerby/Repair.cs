using UnityEngine;
using System.Collections;

public class Repair : MonoBehaviour 
{
	// Pointer to deformable component in the cube
	Deformable deformable;

	void Awake () 
	{
		// Capture deformable component
		deformable = GetComponent<Deformable>();	
	}
	
	void Update () 
	{
		// Use delimited repair on mouse click
		if (Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.collider.transform.parent == transform)
				{
					deformable.Repair(Time.deltaTime, hit.point, 1);	
				}
			}	
		}	
	}
}
