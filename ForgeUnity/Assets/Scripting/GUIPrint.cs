using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIPrint : MonoBehaviour
{
	private Vector3 PlateBounds = new Vector3(225, 145, 150); //225 * 145 * 150mm
	private Vector3 ObjectBounds = Vector3.zero;

    //Use this if not attached to any object
    //private void OnGUI()
    //{
    //    if (GUI.Button(new Rect(15, 15, 100, 50), "Print"))
    //    {
    //        GameObject t = getObjectView(10);
    //        if (t != null)
    //        {
    //            if (t.tag == ("Pickup"))
    //            {
    //                Debug.LogError("found");
    //                MeshFilter[] mesh = getMeshFilter(t);
    //                Debug.Log(STL.ExportBinary(mesh));
    //            }
    //            else
    //                Debug.LogError("Invalid");
    //        }
    //        else
    //            Debug.LogError("No Object found");
    //    }
    //}
    //Use this if attached to object, this may be the optimal method
    private void OnGUI()
    {
        if (GUI.Button(new Rect(15, 15, 100, 50), "Print"))
        {
            MeshFilter[] mesh = getMeshFilter(gameObject);
            Debug.Log(STL.ExportBinary(mesh));
        }
    }

    GameObject getObjectView(float range)
    {
        Vector3 position = gameObject.transform.position;
        RaycastHit ray;
        Vector3 target = position + Camera.main.transform.forward * range;
        if (Physics.Linecast(position, target, out ray))
            return ray.collider.gameObject;
        else
            return null;
    }

    //
    MeshFilter[] getMeshFilter(GameObject Selection)
    {
		Vector3 min = new Vector3 (float.MaxValue, float.MaxValue, float.MaxValue);
		Vector3 max = new Vector3 (float.MinValue, float.MinValue, float.MinValue);

        GameObject objects = Selection;
        List<MeshFilter> filterList = new List<MeshFilter>();
        MeshFilter[] filters = objects.GetComponentsInChildren<MeshFilter>();
        for (int f = 0; f < filters.Length; f++)
        {
			MeshFilter mesh = filters[f];

			if (mesh != null)
            {
				min = Vector3.Min(min, mesh.mesh.bounds.min);
				max = Vector3.Min(max, mesh.mesh.bounds.max );
          		filterList.Add(mesh);
            }
        }

		ObjectBounds = max - min;
		Debug.Log (ObjectBounds);

        return filterList.ToArray();
    }
}
