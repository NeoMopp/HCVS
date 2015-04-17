using UnityEngine;
using System.Collections;

public class ColourChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.renderer.material.color = Color.black;
	
	}
	
	// Update is called once per frame
	void Update () {
        if ((gameObject.GetComponent<Deformable>()).Hardness >= 0.05f)
        {
            gameObject.renderer.material.color = Color.red;
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness >= 0.1f)
        {
            gameObject.renderer.material.color = Color.black;
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness > 0.15f)
        { 
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness >= 0.2f)
        { 
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness >= 0.25f)
        {
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness >= 0.3f)
        { 
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness >= 0.35f)
        { 
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness >= 0.4f)
        { 
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness >= 0.45f)
        { 
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness >= 0.5f)
        { 
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness >= 0.55f)
        { 
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness >= 0.65f)
        { 
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness >= 0.7f)
        {
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness >= 0.75f)
        {
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness >= 0.8f)
        {
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness >= 0.85f)
        {
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness >= 0.9f)
        {
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness >= 0.95f)
        {
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness >= 1.0f)
        {
        }
	
	}
}
