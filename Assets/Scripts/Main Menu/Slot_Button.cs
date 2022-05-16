using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot_Button : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public GameObject playtimeText;
    public GameObject loadBtn;
    public GameObject deleteBtn;

    public GameObject confirmText;
    public GameObject confirmBtn;
    public GameObject NoBtn;

    public GameObject Arrow;

    EventSystem m_EventSystem;

    FMOD.Studio.EventInstance movementUI;

    public void PlayMovementUIEvent()
    {
        movementUI = FMODUnity.RuntimeManager.CreateInstance("event:/UI/moveSelection");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(movementUI, transform);
        movementUI.start();
        movementUI.release();
    }

    public void OnSelect(BaseEventData eventData)
    {
        PlayMovementUIEvent();
        Arrow.SetActive(true);
    }

    public void OnDeselect(BaseEventData data)
    {
        Arrow.SetActive(false);
    }

    void OnEnable()
    {
        m_EventSystem = EventSystem.current;
    }


    private void Update()
    {
        if(Input.GetButtonDown("Cancel") && (m_EventSystem.currentSelectedGameObject == loadBtn || m_EventSystem.currentSelectedGameObject == deleteBtn))
        {
            ChangeToSelect();
        }

        if(Input.GetButtonDown("Cancel") && (m_EventSystem.currentSelectedGameObject == NoBtn || m_EventSystem.currentSelectedGameObject == confirmBtn))
        {
            ChangeToLoad();
        }
    }

    public void ChangeToLoad()
    {
        loadBtn.SetActive(true);
        deleteBtn.SetActive(true);

        confirmText.SetActive(false);
        confirmBtn.SetActive(false);
        NoBtn.SetActive(false);

        loadBtn.GetComponent<Button>().Select();
    }

    public void ChangeToSelect()
    {
        loadBtn.SetActive(false);
        deleteBtn.SetActive(false);

        gameObject.GetComponent<Button>().Select();
    }

    public void ChangeToConfirm()
    {
        loadBtn.SetActive(false);
        deleteBtn.SetActive(false);

        confirmText.SetActive(true);
        confirmBtn.SetActive(true);
        NoBtn.SetActive(true);

        NoBtn.GetComponent<Button>().Select();
    }
}
