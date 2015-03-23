using UnityEngine;
using System.Collections;

public class LockCamera : MonoBehaviour {
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.LogError("Esc");
            {
                GetComponent<MouseLook>().enabled = !GetComponent<MouseLook>().enabled;
            }
        }
    }
}
