using UnityEngine;
using System.Collections;

public class MetalRain : MonoBehaviour 
{
	// Rain prefab
	public GameObject RainObject;
	// Hardness map
	public Texture2D HardnessMap;

	// Pointer to deformable component in the cube
	Deformable deformable;

	// Flag of hardness map usage
	bool map = false;
	
	// Flag of hardness map visible
	bool showMap = false;

	void Start () 
	{
		// Start rain procedures
		StartCoroutine(Rain());
		
		// Capture deformable component
		deformable = GetComponent<Deformable>();	
	}
	
	IEnumerator Rain () 
	{
		while (true)
		{
			// Create random drops
			GameObject.Instantiate(RainObject, new Vector3((Random.value - 0.5f) * 20, (Random.value * 40) + 10, (Random.value - 0.5f) * 20), Quaternion.identity);
			yield return new WaitForSeconds(Random.value * 0.2f + 0.1f);
		}
	}

	void OnMouseDown ()
	{
		// Repair 25% of damage on click
		deformable.Repair(0.25f);
	}

	void OnGUI ()
	{
		// Gui controls
		GUILayout.Label("Hardness:");
		deformable.Hardness = GUILayout.HorizontalSlider(deformable.Hardness, 0.1f, 1);
		deformable.DeformMeshCollider = GUILayout.Toggle(deformable.DeformMeshCollider, " Update mesh collider (high CPU usage)");
		bool b = GUILayout.Toggle(map, " Use hardness map");
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
			showMap = GUILayout.Toggle(showMap, " Show hardness map");
			if (showMap)
				renderer.material.mainTexture = HardnessMap;
			else
				renderer.material.mainTexture = null;			
		}
		
		GUILayout.Label("Click on the object to repair it");
	}
}
