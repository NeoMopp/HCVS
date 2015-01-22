using UnityEngine;
using System.Collections;

public class UserActions : MonoBehaviour {
    Animator animationController;
    public GameObject obj1, obj2, obj3;


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

        if (Input.GetKeyDown("q"))
        {
            animationController.SetBool("Holding", true);
            changeObject();
        }
        if (Input.GetKeyDown("1"))
        {
            animationController.SetBool("Holding", true);
            changeObject(1);
        }
        if (Input.GetKeyDown("2"))
        {
            animationController.SetBool("Holding", true);
            changeObject(2);
        }
        if (Input.GetKeyDown("3"))
        {
            animationController.SetBool("Holding", true);
            changeObject(3);
        }
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

    void changeObject()
    {
        if (obj1.activeSelf)
        {
            obj1.SetActive(false);
            obj2.SetActive(true);
            //animationController.SetBool("Holding", true);
        }

        else
        {
            obj2.SetActive(false);
            obj1.SetActive(true);
            //animationController.SetBool("Holding", true);
        }
    }

    void changeObject(int num)
    {
        switch (num) { 
            case 1:
                obj1.SetActive(true);
                obj2.SetActive(false);
                obj3.SetActive(false);
                break;
            case 2:
                obj1.SetActive(false);
                obj2.SetActive(true);
                obj3.SetActive(false);
                break;
            case 3:
                obj1.SetActive(false);
                obj2.SetActive(false);
                obj3.SetActive(true);
                break;
            default:
                obj1.SetActive(false);
                obj2.SetActive(false);
                obj3.SetActive(false);
                break;

        }

    }
}
