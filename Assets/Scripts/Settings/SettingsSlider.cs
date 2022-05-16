using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsSlider : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    GameObject whiteSquare;
    EventSystem m_EventSystem;
    public Button parent;

    void OnEnable()
    {
        m_EventSystem = EventSystem.current;
    }

    private void Start()
    {
        whiteSquare = this.transform.GetChild(0).gameObject;
    }

    public void OnSelect(BaseEventData eventData)
    {
        whiteSquare.SetActive(true);
    }

    public void OnDeselect(BaseEventData data)
    {
        whiteSquare.SetActive(false);
    }
}
