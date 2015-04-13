#pragma strict

// Rain prefab
var RainObject: GameObject;
// Hardness map
var HardnessMap: Texture2D;

// Pointer to deformable component in the cube
private var deformable: S_Deformable;

// Flag of hardness map usage
private var map: boolean = false;

// Flag of hardness map visible
private var showMap: boolean = false;

function Start () 
{
	// Start rain procedures
	Rain();
	
	// Capture deformable component
	deformable = GetComponent(S_Deformable);	
}

function Rain () 
{
	while (true)
	{
		// Create random drops
		GameObject.Instantiate(RainObject, Vector3((Random.value - 0.5) * 20, (Random.value * 40) + 10, (Random.value - 0.5) * 20), Quaternion.identity);
		yield WaitForSeconds(Random.value * 0.2 + 0.1);
	}
}

function OnMouseDown ()
{
	// Repair 25% of damage on click
	deformable.Repair(0.25);
}

function OnGUI ()
{
	// Gui controls
	GUILayout.Label('Hardness:');
	deformable.Hardness = GUILayout.HorizontalSlider(deformable.Hardness, 0.1, 1);
	deformable.DeformMeshCollider = GUILayout.Toggle(deformable.DeformMeshCollider, ' Update mesh collider (high CPU usage)');
	var b: boolean = GUILayout.Toggle(map, ' Use hardness map');
	if (b != map)
	{
		map = b;
		deformable.Repair(1);
		if (map)
		{
			deformable.HardnessMap = HardnessMap;
		}
		else
		{
			deformable.HardnessMap = null;
			renderer.material.mainTexture = null;
			showMap = false;
		}
	}
	if (map)
	{
		showMap = GUILayout.Toggle(showMap, ' Show hardness map');
		if (showMap)
			renderer.material.mainTexture = HardnessMap;
		else
			renderer.material.mainTexture = null;			
	}
		
	GUILayout.Label('Click on the object to repair it');
}