#pragma strict

// Pointer to deformable component in the cube
private var deformable: S_Deformable;

function Awake () 
{
	// Capture deformable component
	deformable = GetComponent(S_Deformable);	
}

function Update () 
{
	// Use delimited repair on mouse click
	if (Input.GetMouseButton(0))
	{
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		var hit : RaycastHit;
		if (Physics.Raycast(ray, hit))
		{
			if (hit.collider.transform.parent == transform)
			{
				deformable.Repair(Time.deltaTime, hit.point, 1);	
			}
		}	
	}
}