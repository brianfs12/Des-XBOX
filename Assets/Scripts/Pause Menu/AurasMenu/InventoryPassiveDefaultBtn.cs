using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryPassiveDefaultBtn : MonoBehaviour
{
    EventSystem m_EventSystem;

    private void OnEnable()
    {
        m_EventSystem = EventSystem.current;

        if (gameObject.name == "Passive_btn")
        {
            EventSystem.current.SetSelectedGameObject(null);
            GetComponent<Button>().Select();
        }
    }
}
