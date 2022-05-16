using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour, ISelectHandler
{
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
    }
}
