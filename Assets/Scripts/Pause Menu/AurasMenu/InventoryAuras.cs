using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryAuras : MonoBehaviour
{
    EventSystem m_EventSystem;

    [Header("Buttons")]
    public Button passiveButton;
    public Button spiritButton;
    public Button combatButton;

    [Header("EssencesLists")]
    public GameObject passiveList;
    public GameObject spiritList;
    public GameObject combatList;

    [Header("EssenceDescriptionText")]
    public TextMeshProUGUI description;

    private void OnEnable()
    {
        m_EventSystem = EventSystem.current;
        m_EventSystem.SetSelectedGameObject(null);
        passiveButton.Select();
        description.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        //m_EventSystem.SetSelectedGameObject(null);
    }

    private void Update()
    {
        if(m_EventSystem.currentSelectedGameObject == passiveButton.gameObject)
        {
            passiveList.SetActive(true);
            spiritList.SetActive(false);
            combatList.SetActive(false);
        }
        else if (m_EventSystem.currentSelectedGameObject == spiritButton.gameObject)
        {
            passiveList.SetActive(false);
            spiritList.SetActive(true);
            combatList.SetActive(false);
        }
        else if (m_EventSystem.currentSelectedGameObject == combatButton.gameObject)
        {
            passiveList.SetActive(false);
            spiritList.SetActive(false);
            combatList.SetActive(true);
        }

        if (Input.GetButtonDown("Submit"))
        {
            if(m_EventSystem.currentSelectedGameObject == passiveButton.gameObject && passiveList.transform.childCount > 0)
            {
                passiveList.transform.GetChild(0).GetComponent<Button>().Select();
                ChangeInteractableButtons(false);
            }
            else if (m_EventSystem.currentSelectedGameObject == spiritButton.gameObject && spiritList.transform.childCount > 0)
            {
                spiritList.transform.GetChild(0).GetComponent<Button>().Select();
                ChangeInteractableButtons(false);
            }
            else if (m_EventSystem.currentSelectedGameObject == combatButton.gameObject && combatList.transform.childCount > 0)
            {
                combatList.transform.GetChild(0).GetComponent<Button>().Select();
                ChangeInteractableButtons(false);
            }
        }
    }

    public void ChangeInteractableButtons(bool _b)
    {
        if(_b)
        {
            description.gameObject.SetActive(false);
        }
        else
        {
            description.gameObject.SetActive(true);
        }
        
        passiveButton.interactable = _b;
        spiritButton.interactable = _b;
        combatButton.interactable = _b;
    }
}
