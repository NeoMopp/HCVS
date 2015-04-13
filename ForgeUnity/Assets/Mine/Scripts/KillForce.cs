using UnityEngine;
using System.Collections;

public class KillForce : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("Killforce", 1);
	}

	void Killforce () {
	Destroy (gameObject);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
