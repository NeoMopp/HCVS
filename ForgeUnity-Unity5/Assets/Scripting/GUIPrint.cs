using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIPrint : MonoBehaviour {

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
        GameObject objects = Selection;
        List<MeshFilter> filterList = new List<MeshFilter>();
        MeshFilter[] filters = objects.GetComponentsInChildren<MeshFilter>();
        for (int f = 0; f < filters.Length; f++)
        {
            if (filters[f] != null)
            {
                filterList.Add(filters[f]);
            }
        }
        return filterList.ToArray();
    }
}
