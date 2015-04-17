using UnityEngine;
using System.Collections;

public class MyoLine : MonoBehaviour
{
    public GameObject Myo;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.GetComponent<LineRenderer>().SetPosition(1, Myo.transform.forward);
	}
}
