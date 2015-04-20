using UnityEngine;
using System.Collections;

public class ControllerInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("360_AButton")){
			}
		if (Input.GetButtonDown ("360_BButton")){
			}
		if (Input.GetButtonDown ("360_XButton")){
		}
		if (Input.GetButtonDown ("360_YButton")){
		}
		if (Input.GetButtonDown ("360_LBumper")){
		}
		if (Input.GetButtonDown ("360_RBumper")){
		}
		if (Input.GetButtonDown ("360_StartButton")){
		}
		if (Input.GetButtonDown ("360_BackButton")){
		}
		if (Input.GetAxis("360_RightTrigger") > 0){
				Debug.Log("Right Trigger");
			}
		if (Input.GetAxis("360_LeftTrigger") > 0){
				Debug.Log("Left Trigger");
			}

		transform.Translate(Input.GetAxis("360_RightThumbStick_XAxis"),0,0);

	}
}
