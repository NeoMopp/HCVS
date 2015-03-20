using UnityEngine;
using System.Collections;

public class AnimatorControls : MonoBehaviour {

    public Animator MainMenu, SubMenu;

	// Use this for initialization
	void Start () {
        MainMenu.SetBool("IsHidden", true);
        SubMenu.SetBool("IsHidden", true);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.LogError("Esc");
            {
                openMainMenu();
                
            }
        }
	
	}

    public void openMainMenu()
    {
        MainMenu.SetBool("IsHidden", false);
    }

    public void closeMainMenu()
    {
        MainMenu.SetBool("IsHidden", true);
    }

    public void openSubMenu()
    {
        MainMenu.SetBool("StateChange", true);
        SubMenu.SetBool("IsHidden", false);
    }

    public void closeSubMenu()
    {
        SubMenu.SetBool("IsHidden", true);
        MainMenu.SetBool("StateChange", false);
    }

    public void switchMenu()
    {
        MainMenu.SetBool("IsHidden", !MainMenu.GetBool("IsHidden"));
    }
}
