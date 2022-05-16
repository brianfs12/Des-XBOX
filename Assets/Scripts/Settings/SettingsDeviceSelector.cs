using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SettingsDeviceSelector : MonoBehaviour
{
    public List<string> devices = new List<string>();

    public GameObject gamepad_panel;
    public GameObject keyboard_panel;

    int index;

    public TextMeshProUGUI deviceText;

    public Button deviceBtn;

    //GameObject whiteSquare;

    public void OnSelect(BaseEventData eventData)
    {
       // whiteSquare.SetActive(true);
    }

    public void OnDeselect(BaseEventData data)
    {
       // whiteSquare.SetActive(false);
    }

    EventSystem m_EventSystem;

    void OnEnable()
    {
        m_EventSystem = EventSystem.current;
    }

    void Start()
    {
        index = 0;

       // whiteSquare = this.transform.GetChild(0).gameObject;

        ChangeResolutionText(index);
    }

    private void Update()
    {
        if (m_EventSystem.currentSelectedGameObject == deviceBtn.gameObject)
        {
            if (Input.GetAxisRaw("Horizontal") == 1 && index < 1)
            {
                index++;
                ChangeResolutionText(index);
            }
            if (Input.GetAxisRaw("Horizontal") == -1 && index > 0)
            {
                index--;
                ChangeResolutionText(index);
            }
        }
    }

    public void ChangeResolutionText(int _currentIndex)
    {
        deviceText.text = devices[_currentIndex];

        if(_currentIndex == 0)
        {
            gamepad_panel.SetActive(true);
            keyboard_panel.SetActive(false);
        }
        else
        {
            gamepad_panel.SetActive(false);
            keyboard_panel.SetActive(true);
        }
    }
}
