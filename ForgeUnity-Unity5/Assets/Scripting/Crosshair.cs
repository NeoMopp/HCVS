using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {

    public Texture2D cross;

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width/2 - 25 ,Screen.height/2 - 25,50,50),"X");
    }
}
