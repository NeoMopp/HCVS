using UnityEngine;
using System.Collections;

public class UserActions : MonoBehaviour {
    Animator animationController; 
	// Use this for initialization
	void Start () {
        animationController = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
            animationController.SetTrigger("UserAction");

        if (Input.GetKeyDown("e"))
            
        animationController.SetBool("Holding", !animationController.GetBool("Holding"));
	}

    GameObject getObjectView(float range)
    {
        Vector3 position = gameObject.transform.position;
        RaycastHit ray;
        Vector3 target = position + Camera.main.transform.forward * range;
        if (Physics.Linecast(position, target, out ray))
            return ray.collider.gameObject;
        return null;
    }
}
