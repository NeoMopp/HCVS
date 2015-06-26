using UnityEngine;
using System.Collections;

public class FireCollisions : MonoBehaviour {   

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other){
        if (other.gameObject.name == "DeformableCube")
        {
            other.GetComponentInChildren<MetalTemperature>().setinFire(true);
        }
    }

    void OnTriggerStay(Collider other){
        if (other.gameObject.name == "DeformableCube")
        {
        }
    }


    void OnTriggerExit(Collider other){
        if (other.gameObject.name == "DeformableCube")
        {
            //Debug.Log("Mesh Exited the trigger");
            other.GetComponentInChildren<MetalTemperature>().setinFire(false);
        }
       

    }

}
