using UnityEngine;
using System.Collections;

public class OnHit : MonoBehaviour {

	public Rigidbody projectile;
	public Transform spawn1;

	void Start() {
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.name == "DeformableCube"/* && collision.relativeVelocity.magnitude > 1 */)
		{
						Rigidbody clone;
						clone = Instantiate (projectile, spawn1.position, spawn1.rotation) as Rigidbody;
						clone.velocity = spawn1.TransformDirection (Vector3.up * 4);
						}
		}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown("Fire1")) {
			Rigidbody clone;
			clone = Instantiate(projectile, spawn1.position, spawn1.rotation) as Rigidbody;
			clone.velocity = spawn1.TransformDirection(Vector3.up * 4);
			Invoke("KillForce", 1);
		}
	
	}
	void KillForce () {
		//Destroy (clone);
	}
}
