using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MovesetBtn : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    GameObject selector;

    [Header("DescriptionBox")]
    public string description;
    public TextMeshProUGUI descriptionText;
    public Image buttonImg;
    public Sprite buttonSprite;

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
        selector = transform.GetChild(0).gameObject;
    }

    private void OnEnable()
    {
        m_EventSystem = EventSystem.current;
        if (gameObject.name == "Shoot")
        {
            selector.SetActive(true);
            GetComponent<Button>().Select();
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        selector.SetActive(true);
        descriptionText.text = description;
        buttonImg.sprite = buttonSprite;
    }

    public void OnDeselect(BaseEventData data)
    {
        selector.SetActive(false);
    }
}
