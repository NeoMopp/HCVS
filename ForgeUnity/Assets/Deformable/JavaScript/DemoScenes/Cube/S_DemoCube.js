#pragma strict

// Pointer to deformable component in the cube
private var deformable: S_Deformable;

function Awake () 
{
	// Capture deformable component
	deformable = GetComponent(S_Deformable);	
}

function OnMouseDown () 
{
	// Repair 25% of damage on click
	deformable.Repair(0.25);
}

function OnGUI ()
{
	// Shows help
	GUILayout.Label('Click on the cube to repair it');
}