using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EmptyFile_Button : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public GameObject startBtn;

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

    void OnEnable()
    {
        m_EventSystem = EventSystem.current;
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

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && m_EventSystem.currentSelectedGameObject == startBtn)
        {
            ChangeToSelect();
        }
    }

    public void ChangeToStart()
    {
        startBtn.SetActive(true);
        startBtn.GetComponent<Button>().Select();
    }


    public void ChangeToSelect()
    {
        startBtn.SetActive(false);
        this.GetComponent<Button>().Select();
    }
}
