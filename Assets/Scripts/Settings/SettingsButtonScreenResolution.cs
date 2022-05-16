using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingsButtonScreenResolution : MonoBehaviour
{
    public Button slider;
    Button me;
    GameObject selector;

    EventSystem m_EventSystem;

    void OnEnable()
    {
        m_EventSystem = EventSystem.current;
    }

    private void Start()
    {
        me = this.GetComponent<Button>();
        selector = this.transform.GetChild(0).gameObject;
    }

    public void SelectSlider()
    {
        slider.Select();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && m_EventSystem.currentSelectedGameObject == slider.gameObject)
        {
            me.Select();
        }

        if (m_EventSystem.currentSelectedGameObject == slider.gameObject && !selector.activeInHierarchy)
        {
            selector.SetActive(true);
        }
    }

    public void SelectButton()
    {
        me.Select();
    }
}
