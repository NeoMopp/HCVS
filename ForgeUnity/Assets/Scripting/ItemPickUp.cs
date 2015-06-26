using UnityEngine;
using System.Collections;

public class ItemPickUp : MonoBehaviour {

    public AudioClip objectDetails;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.name == "RightHand" && this.name != "Forge_Nippers"){
            //Drop tools
        }
        else if (other.name == "LeftHand" && this.name == "Forge_Nippers"){
        }

    }


    void OnTriggerExit(Collider other) { 
    
    }

    void OnTriggerStay(Collider other ){
        if (other.name == "RightHand" && this.name != "Forge_Nippers"){
            //Drop tools
            //objectDetails
            other.GetComponent(this.GetType()).active = true;
            Debug.LogError("Object picked up");
            //audio.PlayOneShot(objectDetails);
        }
        else if (other.name == "LeftHand" && this.name == "Forge_Nippers"){
            //PickUpTongs
            audio.PlayOneShot(objectDetails);
        }
    
    }
}
