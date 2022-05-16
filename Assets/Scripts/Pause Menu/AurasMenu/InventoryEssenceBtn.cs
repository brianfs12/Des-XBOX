using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryEssenceBtn : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [HideInInspector]
    public string description;
    public int quantity;
    [HideInInspector]
    public string name;

    public string path;

    GameObject selector;
    public GameObject blueEquipImg;
    public GameObject greenEquipImg;
    public GameObject redEquipImg;
    InventoryAuras buttons;

    public TextMeshProUGUI quantityText;

    EventSystem m_EventSystem;

    float delay = 0.0f; //Pequeño delay al pasar a la seleccion de esencia para que no se equipe sola

    private void OnEnable()
    {
        m_EventSystem = EventSystem.current;

        blueEquipImg = transform.GetChild(1).gameObject;
        greenEquipImg = transform.GetChild(4).gameObject;
        redEquipImg = transform.GetChild(5).gameObject;
        selector = transform.GetChild(0).gameObject;
        buttons = GetComponentInParent<InventoryAuras>();
        quantityText = transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void OnDisable()
    {
        delay = 0.0f;
        selector.SetActive(false);
    }

    private void Update()
    {
        if (m_EventSystem.currentSelectedGameObject == this.gameObject && delay <= 70.0f)
        {
            delay += Time.unscaledTime;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if(transform.parent.gameObject.name == "PassiveEssencesList")
            {
                buttons.ChangeInteractableButtons(true);
                buttons.passiveButton.Select();
                buttons.passiveButton.GetComponent<InventoryAuraSelect>().selectText.SetActive(true);
            }
            else if (transform.parent.gameObject.name == "SpiritEssencesList")
            {
                buttons.ChangeInteractableButtons(true);
                buttons.spiritButton.Select();
                buttons.spiritButton.GetComponent<InventoryAuraSelect>().selectText.SetActive(true);
            }
            else if (transform.parent.gameObject.name == "CombatEssencesList")
            {
                buttons.ChangeInteractableButtons(true);
                buttons.combatButton.Select();
                buttons.combatButton.GetComponent<InventoryAuraSelect>().selectText.SetActive(true);
            }
        }

        if(Input.GetButtonDown("Unequip"))
        {
            if (m_EventSystem.currentSelectedGameObject == this.gameObject)
            {
                if (transform.parent.gameObject.name == "PassiveEssencesList")
                {
                    GameManager.Instance.player.GetComponent<PlayerAuraLoadouts>().UnequipPassiveEssences(this);
                }
                else if (transform.parent.gameObject.name == "SpiritEssencesList")
                {
                    GameManager.Instance.player.GetComponent<PlayerAuraLoadouts>().UnquipSpiritEssence(this);
                }
                else if (transform.parent.gameObject.name == "CombatEssencesList")
                {
                    GameManager.Instance.player.GetComponent<PlayerAuraLoadouts>().UnquipCombatEssence(this);
                }
            }
        }

        if(Input.GetButtonDown("Submit"))
        {
            if (m_EventSystem.currentSelectedGameObject == this.gameObject && delay > 70f)
            {
                if (transform.parent.gameObject.name == "PassiveEssencesList")
                {
                    GameManager.Instance.player.GetComponent<PlayerAuraLoadouts>().ChangePassiveEssences(this);
                }
                else if (transform.parent.gameObject.name == "SpiritEssencesList")
                {
                    GameManager.Instance.player.GetComponent<PlayerAuraLoadouts>().ChangeSpiritEssence(this);
                }
                else if (transform.parent.gameObject.name == "CombatEssencesList")
                {
                    GameManager.Instance.player.GetComponent<PlayerAuraLoadouts>().ChangeCombatEssence(this);
                }
            }
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        selector.SetActive(true);
        buttons.description.text = description;

        if (transform.parent.gameObject.name == "PassiveEssencesList")
        {
            buttons.passiveButton.GetComponent<InventoryAuraSelect>().selectText.SetActive(true);
            //GetComponent<Button>().onClick.AddListener(delegate { GameManager.Instance.player.GetComponent<PlayerAuraLoadouts>().ChangePassiveEssences(this); });
        }
        else if (transform.parent.gameObject.name == "SpiritEssencesList")
        {
            buttons.spiritButton.GetComponent<InventoryAuraSelect>().selectText.SetActive(true);
            //GetComponent<Button>().onClick.AddListener(delegate { GameManager.Instance.player.GetComponent<PlayerAuraLoadouts>().ChangeSpiritEssence(this); });
        }
        else if (transform.parent.gameObject.name == "CombatEssencesList")
        {
            buttons.combatButton.GetComponent<InventoryAuraSelect>().selectText.SetActive(true);
            //GetComponent<Button>().onClick.AddListener(delegate { GameManager.Instance.player.GetComponent<PlayerAuraLoadouts>().ChangeCombatEssence(this); });
        }
    }

    public void OnDeselect(BaseEventData data)
    {
        selector.SetActive(false);
        delay = 0.0f;
        //GetComponent<Button>().onClick.RemoveAllListeners();
    }

}
