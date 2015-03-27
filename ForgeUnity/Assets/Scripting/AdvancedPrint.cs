using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using UDebug = UnityEngine.Debug;

public static class AdvancedPrint
{
	//Table coords:
	//X = Front
	//Y = Up
	//Z = Left

	//Printer coords:
	//X = Left/Right
	//Y = Up
	//Z = Front/Back

	public static Vector3 PlateBounds = new Vector3(225, 145, 150); //225 * 145 * 150mm
	public static Vector3 ObjectBounds = Vector3.zero; //1 Unity unit = 1mm
	public static float ObjectHeight = 0;
	public static float MasterScale = 0.5f;

	public static string Print(GameObject gameObject)
	{
		MeshFilter[] mesh = getMeshFilter(gameObject);

		if (mesh == null || mesh.Length == 0)
		{
			UDebug.Log ("No meshes found on object.");
			return null;
		}
		else if (ObjectBounds.magnitude == 0)
		{
			UDebug.Log("Object has bounds of zero.");
			return null;
		}

		STL.ObjectScale = Mathf.Min(Mathf.Min(PlateBounds.x / ObjectBounds.x, PlateBounds.y / ObjectBounds.y), PlateBounds.z / ObjectBounds.z) * Mathf.Clamp (MasterScale, 0, 1);
		STL.HeightOffset = ObjectHeight;

		string path = STL.ExportText(mesh);

		if (path == null || path.Length == 0)
		{
			UDebug.Log("STL Export returned invalid path.");
			return null;
		}

		string currDir = Directory.GetCurrentDirectory ();
		string Slic3rPath = Path.Combine (currDir, "Slic3r\\slic3r.exe");
		string configPath = Path.Combine(currDir, "Slic3r\\WanhaoDup4Config.ini");
		Process.Start(Slic3rPath, path + " --load " + configPath);

		return path.Substring(0, path.Length - 4) + ".GCODE";
	}

	private static MeshFilter[] getMeshFilter(GameObject gameObject)
	{
		ObjectBounds = Vector3.zero;

		List<MeshFilter> validFilters = new List<MeshFilter>();
		foreach (MeshFilter meshFilter in gameObject.GetComponentsInChildren<MeshFilter>())
		{
			if (meshFilter != null)
			{
				ObjectBounds = STL.CoordTransform * meshFilter.mesh.bounds.size;
				ObjectHeight = meshFilter.mesh.bounds.min.y;

				validFilters.Add(meshFilter);
			}
		}

		ObjectBounds.x = Mathf.Abs(ObjectBounds.x);
		ObjectBounds.y = Mathf.Abs(ObjectBounds.y);
		ObjectBounds.z = Mathf.Abs(ObjectBounds.z);
		ObjectHeight = Mathf.Abs(ObjectHeight);

		return validFilters.ToArray();
	}
}
