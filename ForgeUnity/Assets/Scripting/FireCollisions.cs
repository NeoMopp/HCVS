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
        Debug.Log("Mesh Entered the trigger");
        deform = other.GetComponent<Deformable>();
    }

    void OnTriggerStay(Collider other){
        Debug.Log("Mesh is within the trigger");
        deform.Hardness = deform.Hardness += 0.1f;
        Debug.Log(deform.Hardness);
    }

    void OnTriggerExit(Collider other){
        Debug.Log("Mesh Exited the trigger");

    }

}
