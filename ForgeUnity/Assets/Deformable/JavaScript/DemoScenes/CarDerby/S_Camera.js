#pragma strict

// Target car
var Car: GameObject;

function Update () 
{
	// Follow target car
	transform.LookAt(Car.transform);
}