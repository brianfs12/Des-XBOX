using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.EventSystems;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    int popUpIndex;


    public GameObject gamepadControls;
    public GameObject keyboardControls;

    bool isOnKeyboard;

    // Start is called before the first frame update
    void Start()
    {

    }

    void onInputDeviceChange(InputUser user, InputUserChange change, InputDevice device)
    {
        if (change == InputUserChange.ControlSchemeChanged)
        {
            updateButtonImage(user.controlScheme.Value.name);
        }
    }

    void updateButtonImage(string schemeName)
    {
        if (schemeName.Equals("Gamepad") && gamepadControls != null && keyboardControls != null)
        {
            isOnKeyboard = false;
            gamepadControls.SetActive(true);
            keyboardControls.SetActive(false);
        }
        else if (!schemeName.Equals("Gamepad") && gamepadControls != null && keyboardControls != null)
        {
            isOnKeyboard = true;
            gamepadControls.SetActive(false);
            keyboardControls.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[popUpIndex].SetActive(true);
            } else
            {
                popUps[popUpIndex].SetActive(false);
            }
            if (popUpIndex == 0)
            {

            }
        }
    }
}
