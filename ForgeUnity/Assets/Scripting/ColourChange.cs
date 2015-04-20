using UnityEngine;
using System.Collections;

public class ColourChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.renderer.material.color = Color.black;
	
	}
	
	// Update is called once per frame
	void Update () {
        if ((gameObject.GetComponent<Deformable>()).Hardness <= 0.05f)
        {
            gameObject.renderer.material.color = new Color32(243, 237, 168, 255);
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness <= 0.1f)
        {
            gameObject.renderer.material.color = new Color32(186, 132, 96, 255);
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness <= 0.15f)
        {
            gameObject.renderer.material.color = new Color32(128, 62, 62, 255);
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness <= 0.2f)
        {
            gameObject.renderer.material.color = new Color32(125, 0, 14, 255);
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness <= 0.25f)
        {
            gameObject.renderer.material.color = new Color32(132, 0, 132, 255);
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness <= 0.3f)
        {
            gameObject.renderer.material.color = new Color32(93, 3, 102, 255);
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness <= 0.35f)
        {
            gameObject.renderer.material.color = new Color32(6, 37, 100, 255);
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness <= 0.4f)
        {
            gameObject.renderer.material.color = new Color32(81, 80, 80, 255);
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness <= 0.45f)
        {
            gameObject.renderer.material.color = new Color32(120, 95, 91, 255);
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness <= 0.5f)
        {
            gameObject.renderer.material.color = new Color32(151, 85, 73, 255);
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness <= 0.55f)
        {
            gameObject.renderer.material.color = new Color32(180, 48, 25, 255);
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness <= 0.65f)
        {
            gameObject.renderer.material.color = new Color32(198, 44, 31, 255);
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness <= 0.7f)
        {
            gameObject.renderer.material.color = new Color32(215, 23, 21, 255);
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness <= 0.75f)
        {
            gameObject.renderer.material.color = new Color32(255, 0, 0, 255);
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness <= 0.8f)
        {
            gameObject.renderer.material.color = new Color32(247, 96, 27, 255);
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness <= 0.85f)
        {
            gameObject.renderer.material.color = new Color32(255, 127, 0, 255);
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness <= 0.9f)
        {
            gameObject.renderer.material.color = new Color32(248, 162, 77, 255);
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness <= 0.95f)
        {
            gameObject.renderer.material.color = new Color32(227, 179, 9, 255);
        }
        else if ((gameObject.GetComponent<Deformable>()).Hardness <= 1.0f)
        {
            gameObject.renderer.material.color = new Color32(255,255,0,255);
        }
	
	}
}
