using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundController : MonoBehaviour {
    public AudioClip[] audioFiles = new AudioClip[6];
    private List<int> tList = new List<int>();
    float counter = 60.0f;
	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {
        counter -= Time.deltaTime;
        if (counter <= 0.0f){
            while (true)
            {
                int t = Random.Range(0, 7);
                if (tList.Contains(t) == false)
                {
                    tList.Add(t);
                    audio.PlayOneShot(audioFiles[t]);
                    counter = 60.0f;
                    break;
                }
            }

        }

	
	}
}
