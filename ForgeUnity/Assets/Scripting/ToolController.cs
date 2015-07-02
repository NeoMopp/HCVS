using UnityEngine;
//using UnityEditor;
using System.Collections;

public class ToolController : MonoBehaviour {

    public GameObject toolA, toolB;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Sets the currently in use tool.
    public void setTool(string tName)
    {
        if (toolA.name == tName)
        {
            toolA.SetActive(true);
            if (toolB != null)
            {
                toolB.SetActive(false); 
            }
        }

        else if (toolB.name == tName)
        {
            toolB.SetActive(true);
            if (toolA != null)
            {
                toolA.SetActive(false);
            }
        }
    }
}
