using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingsButtons : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    GameObject selector;
    EventSystem m_EventSystem;
    FMOD.Studio.EventInstance movementUI;

    public void PlayMovementUIEvent()
    {
        movementUI = FMODUnity.RuntimeManager.CreateInstance("event:/UI/moveSelection");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(movementUI, transform);
        movementUI.start();
        movementUI.release();
    }

    private void Awake()
    {
        selector = gameObject.transform.GetChild(0).gameObject;
    }

    private void OnEnable()
    {
        m_EventSystem = EventSystem.current;

        if (gameObject.name == "SFX_btn")
        {
            gameObject.GetComponent<Button>().Select();
        }
        if (gameObject.name == "Device_btn")
        {
            gameObject.GetComponent<Button>().Select();
            gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 77f, 0);
        }
    }

    private void OnDisable()
    {
        //m_EventSystem.SetSelectedGameObject(null);
        selector.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {
        PlayMovementUIEvent();
        selector.SetActive(true);
    }

    public void OnDeselect(BaseEventData data)
    {
        selector.SetActive(false);
    }


}
