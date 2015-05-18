using UnityEngine;
using System.Collections;

public class FireCollisions : MonoBehaviour {

    Deformable deform;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other){
        if (other.gameObject.name == "DeformableCube")
        {
            Debug.Log("Mesh Entered the trigger");
            deform = other.GetComponent<Deformable>();
        }
    }

    void OnTriggerStay(Collider other){
        if (other.gameObject.name == "DeformableCube")
        {
            Debug.Log("Mesh is within the trigger");
            if (deform.Hardness > 0.1f)
            {
                deform.Hardness -= 0.01f;
            }
            //Debug.Log(deform.Hardness);
        }
    }

    void OnTriggerExit(Collider other){
        if (other.gameObject.name == "DeformableCube")
        {
            Debug.Log("Mesh Exited the trigger");
            deform = null;
        }

    }

}
