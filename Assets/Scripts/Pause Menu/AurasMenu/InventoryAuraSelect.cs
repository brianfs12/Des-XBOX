//Resaltar el texto (que es una imagen) del boton al seleccionarlo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InventoryAuraSelect : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public GameObject selectText;
    EventSystem m_EventSystem;

    FMOD.Studio.EventInstance movementUI;

    public void PlayMovementUIEvent()
    {
        movementUI = FMODUnity.RuntimeManager.CreateInstance("event:/UI/moveSelection");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(movementUI, transform);
        movementUI.start();
        movementUI.release();
    }

    private void OnEnable()
    {
        m_EventSystem = EventSystem.current;
    }

    private void Start()
    {
        selectText = gameObject.transform.GetChild(0).gameObject;
    }

    public void OnSelect(BaseEventData eventData)
    {
        PlayMovementUIEvent();
        selectText.SetActive(true);
    }

    public void OnDeselect(BaseEventData data)
    {
        selectText.SetActive(false);
    }

    private void OnDisable()
    {
        selectText.SetActive(false);
    }
}
