using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class AnimatorControls : MonoBehaviour
{

    public Animator MainMenu, SubMenu;
    public GameObject myo = null;
    private Pose _lastPose = Pose.Unknown;
    public GameObject PrintButton = null, GuideButton = null, ResetButton = null, HorseshoeBut = null, HookBut = null, FreeBut = null;
    public GameObject deformObj = null, hookObj = null, horeshoeObj = null;
    private GameObject printObj = null;

    // Use this for initialization
    void Start()
    {
        MainMenu.SetBool("IsHidden", true);
        SubMenu.SetBool("IsHidden", true);
        printObj = deformObj;
    }

    // Update is called once per frame
    void Update()
    {

        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();

        if (thalmicMyo.pose != _lastPose)
        {
            _lastPose = thalmicMyo.pose;

            //This pose enables/diables the menu
            if (thalmicMyo.pose == Pose.DoubleTap && MainMenu.GetBool("IsHidden"))
            {
                openMainMenu();
                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }

            else if (thalmicMyo.pose == Pose.WaveIn && MainMenu.GetBool("IsHidden") == true)
            {
                onWaveIn();
                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }

            else if (thalmicMyo.pose == Pose.WaveOut && MainMenu.GetBool("IsHidden") == true)
            {
                onWaveOut();
                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }

            else if (thalmicMyo.pose == Pose.Fist && MainMenu.GetBool("IsHidden") == true)
            {
                onFist();
                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }

            else if (thalmicMyo.pose == Pose.FingersSpread && MainMenu.GetBool("IsHidden") == true)
            {
                onFingersSpread();
                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape) && MainMenu.GetBool("IsHidden"))
        {
            Debug.LogError("Esc");
            openMainMenu();
        }

        else if (Input.GetKeyUp(KeyCode.Escape) && !MainMenu.GetBool("IsHidden"))
        {
            Debug.LogError("Esc");
            closeSubMenu();
            closeMainMenu();
        }

        if (Input.GetKeyUp(KeyCode.Backspace) && MainMenu.GetBool("StateChange") && !SubMenu.GetBool("IsHidden"))
        {
            Debug.LogError("Backspace");
            closeSubMenu();
        }

        Deformable deform = deformObj.GetComponent<Deformable>();
        if (deform.Hardness < 1.0f)
        {
            deform.Hardness += 0.001f;
        }
    }

    //Shows the main menu
    private void openMainMenu()
    {
        MainMenu.SetBool("IsHidden", false);
        EventSystem.current.SetSelectedGameObject(PrintButton);
    }

    //Hides the main menu
    private void closeMainMenu()
    {
        MainMenu.SetBool("IsHidden", true);
        EventSystem.current.SetSelectedGameObject(null);
    }

    //Shows the sub menu while hiding the main menu
    private void openSubMenu()
    {
        MainMenu.SetBool("StateChange", true);
        SubMenu.SetBool("IsHidden", false);
        EventSystem.current.SetSelectedGameObject(HorseshoeBut);
    }

    //Hides the sub menu while showing the main menu
    private void closeSubMenu()
    {
        SubMenu.SetBool("IsHidden", true);
        MainMenu.SetBool("StateChange", false);
        EventSystem.current.SetSelectedGameObject(PrintButton);
    }

    //Invert the Mainmenu
    private void switchMenu()
    {
        MainMenu.SetBool("IsHidden", !MainMenu.GetBool("IsHidden"));
    }

    // Extend the unlock if ThalmcHub's locking policy is standard, and notifies the given myo that a user action was
    // recognized.
    void ExtendUnlockAndNotifyUserAction(ThalmicMyo myo)
    {
        ThalmicHub hub = ThalmicHub.instance;

        if (hub.lockingPolicy == LockingPolicy.Standard)
        {
            myo.Unlock(UnlockType.Timed);
        }

        myo.NotifyUserAction();
    }

    //On guide button press
    public void GuidePress()
    {
        openSubMenu();

    }


    //On print button press
    public void PrintPress()
    {
        if (printObj != null)
            AdvancedPrint.Print(printObj);

        else
            Debug.LogError("No printObj Assigned");
    }

    //On reset button press
    public void ResetPress()
    {
        Debug.Log("Reset");
        Deformable deform = deformObj.GetComponent<Deformable>();
        deform.Repair(1.0f);
    }

    //On horseshoe button press
    public void HorseShoePress()
    {
        Debug.Log("Horseshoe");
        printObj = horeshoeObj;
    }

    //On hook button press
    public void HookPress()
    {
        Debug.Log("Hook");
        printObj = hookObj;
    }

    //On free button press
    public void FreePress()
    {
        Debug.Log("Free");
        printObj = deformObj;
    }


    //Controlling the menu through myo
    private void onWaveIn()
    {
        if (EventSystem.current.currentSelectedGameObject == GuideButton)
            EventSystem.current.SetSelectedGameObject(PrintButton);
        else if (EventSystem.current.currentSelectedGameObject == ResetButton)
            EventSystem.current.SetSelectedGameObject(GuideButton);
        else if (EventSystem.current.currentSelectedGameObject == HookBut)
            EventSystem.current.SetSelectedGameObject(HorseshoeBut);
        else if (EventSystem.current.currentSelectedGameObject == FreeBut)
            EventSystem.current.SetSelectedGameObject(HookBut);
    }

    private void onWaveOut()
    {
        if (EventSystem.current.currentSelectedGameObject == PrintButton)
            EventSystem.current.SetSelectedGameObject(GuideButton);
        else if (EventSystem.current.currentSelectedGameObject == GuideButton)
            EventSystem.current.SetSelectedGameObject(ResetButton);
        else if (EventSystem.current.currentSelectedGameObject == HorseshoeBut)
            EventSystem.current.SetSelectedGameObject(HookBut);
        else if (EventSystem.current.currentSelectedGameObject == HookBut)
            EventSystem.current.SetSelectedGameObject(FreeBut);
    }

    //Emulation of enter press on button
    private void onFist()
    {
        if (EventSystem.current.currentSelectedGameObject == PrintButton)
            PrintPress();
        else if (EventSystem.current.currentSelectedGameObject == GuideButton)
            GuidePress();
        else if (EventSystem.current.currentSelectedGameObject == ResetButton)
            ResetPress();
        else if (EventSystem.current.currentSelectedGameObject == HorseshoeBut)
            HorseShoePress();
        else if (EventSystem.current.currentSelectedGameObject == HookBut)
            HookPress();
        else if (EventSystem.current.currentSelectedGameObject == FreeBut)
            FreePress();
    }

    private void onFingersSpread()
    {
        if (MainMenu.GetBool("StateChange") && !SubMenu.GetBool("IsHidden"))
            closeSubMenu();
    }

}


