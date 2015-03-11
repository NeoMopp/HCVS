using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIScript : MonoBehaviour 
{
    bool gui = true;

    public void Print()
    {
        MeshFilter[] mesh = getMeshFilter(gameObject);
        Debug.Log(STL.ExportBinary(mesh));
    }

   
   //Creates GUI buttons, relies on script being attached to the intended object.
    private void OnGUI()
    { 
        //Normal menu starts here
        if (gui)
        {
            //Creates the print button, clicking this generates the .stl on the desktop.
            if (GUI.Button(new Rect(15, 15, 60, 30), "Print"))
            {
                MeshFilter[] mesh = getMeshFilter(gameObject);
                Debug.Log(STL.ExportBinary(mesh));
            }

            //Brings up a sub menu that allows for selection of a certain object to craft.
            if (GUI.Button(new Rect(120, 15, 60, 30), "Guide"))
            {
                Debug.Log("Guide");
                gui = !gui;
            }

            //Resets the model back to its standard setting.
            if (GUI.Button(new Rect(230, 15, 60, 30), "Reset"))
            {
                //Some stuff needs to happen to reset the model back to normal.
                Debug.Log("Reset");
            }
        }

        //Submenu for specific objects creation starts here
        else if (!gui)
        {
            //Create Horseshoe
            if (GUI.Button(new Rect(15, 15, 60, 30), "Horseshoe"))
            {
                //Enable horseshoe
                gui = !gui;
            }

            //Create Hook
            if (GUI.Button(new Rect(Screen.width/2 - 50, Screen.height/2 -25, 100, 50), "Hook")) 
            {
                //Enable hook
                gui = !gui;
            }
        }
    }
    
    //Converts the selected object into a mesh which can be converted to .stl format and exported.
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

    //Creates GUI buttons, relies on raycast hit to select any object in the scene. Use this if not attached to any object
    //private void OnGUI(){
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

    //Preforms a raycast hit to get the object for printing.
    //GameObject getObjectView(float range)
    //{
    //    Vector3 position = gameObject.transform.position;
    //    RaycastHit ray;
    //    Vector3 target = position + Camera.main.transform.forward * range;
    //    if (Physics.Linecast(position, target, out ray))
    //        return ray.collider.gameObject;
    //    else
    //        return null;
    //}
}

