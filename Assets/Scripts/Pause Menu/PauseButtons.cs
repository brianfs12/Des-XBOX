using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseButtons : MonoBehaviour, ISelectHandler
{
    public PauseCanvas pause;
    public string description;

    private void OnEnable()
    {
        if(gameObject.name == "Map_btn")
        {
            //EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(gameObject);
            //gameObject.GetComponent<Button>().Select();
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        pause.PlayMovementUIEvent();
        pause.ChangeDescriptionText(description);
    }
}
