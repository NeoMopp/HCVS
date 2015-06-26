using UnityEngine;
using System.Collections;

public class MetalTemperature : MonoBehaviour {

    private bool inFire = false;
    private float coolCount = 20.0f, hotCount = 10.0f;
    public AudioClip burnAudio, notHotEnoughAudio, prefectYellowAudio;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!inFire)
        {
            coolCount -= Time.deltaTime;
            if (coolCount <= 0.0f)
            {
                if (this.GetComponent<Deformable>().Hardness <= 1.0f)
                {
                    this.GetComponent<Deformable>().Hardness += 0.01f;
                }
                coolCount = 20.0f;
            }
        }

        else
        {
            hotCount -= Time.deltaTime;
            if (hotCount <= 0.0f)
            {
                if (this.GetComponent<Deformable>().Hardness >= 0.1f)
                {
                    this.GetComponent<Deformable>().Hardness -= 0.01f;
                }
                hotCount = 10.0f;
            }
        }
	
	}

    //Lets you set the inFire variable
    public void setinFire(bool t)
    {
        inFire = t;
       // Debug.Log("inFire changed");
    }
}
