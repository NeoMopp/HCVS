﻿using UnityEngine;
//using UnityEditor;
using System.Collections;

public class ItemPickUp : MonoBehaviour {

    public AudioClip objectDetails;
    public GameObject handRequired;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        //Debug.LogError(other.name + "  is in the trigger");
        if (gameObject.name == "ArbitraryCube" && other.gameObject == handRequired)
        {
            other.GetComponent<ToolController>().setTool("DeformableCube");
            gameObject.SetActive(false);
            audio.PlayOneShot(objectDetails);
        }
        if (other.gameObject == handRequired)
        {
            //Set the correct tool here
            //play sound
            other.GetComponent<ToolController>().setTool(gameObject.name);
            gameObject.SetActive(false);
            audio.PlayOneShot(objectDetails);
        }

    }


    void OnTriggerExit(Collider other) { 
    
    }

    void OnTriggerStay(Collider other ){

    
    }

    public void setEnabled()
    {
        gameObject.SetActive(true);
    }
}
