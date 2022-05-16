using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAuraLoadouts : MonoBehaviour
{
    public enum Aura{Blue, Green, Red};
    public Aura currentAura;

    [Header("Blue Aura Loadout")]
    public CombatEssence blueCombatEssence;
    public SpiritEssence blueSpiritEssence;
    public List<PassiveEssence> bluePassiveEssence = new List<PassiveEssence>();

    [Header("Green Aura Loadout")]
    public CombatEssence greenCombatEssence;
    public SpiritEssence greenSpiritEssence;
    public List<PassiveEssence> greenPassiveEssence = new List<PassiveEssence>();

    [Header("Red Aura Loadout")]
    public CombatEssence redCombatEssence;
    public SpiritEssence redSpiritEssence;
    public List<PassiveEssence> redPassiveEssence = new List<PassiveEssence>();

    private PlayerSpiritAbilityHolder spiritAbilityHolder;
    private PlayerCombatEssenceHolder combatAbilityHolder;
    private PlayerPassiveEssenceHolder passiveAbilityHolder;

    private void Awake()
    {
        spiritAbilityHolder = GetComponent<PlayerSpiritAbilityHolder>();
        combatAbilityHolder = GetComponent<PlayerCombatEssenceHolder>();
        passiveAbilityHolder = GetComponent<PlayerPassiveEssenceHolder>();
    }

    public void changeLoadoutNext(InputAction.CallbackContext context)
    {
        if (context.performed && !GameManager.Instance.stopPlayer)
        {
            if (spiritAbilityHolder.essenceActive)
            {
                spiritAbilityHolder.essenceActive = false;
                spiritAbilityHolder.spiritEssence.Deactivate(this.gameObject);
            }

            if (currentAura + 1 == Aura.Green && GameManager.Instance.green)
            {
                currentAura += 1;
            }
            else if (currentAura + 1 == Aura.Red && GameManager.Instance.red)
            {
                currentAura += 1;
            }
            else if (currentAura + 1 == Aura.Red && !GameManager.Instance.red)
            {
                currentAura = 0;
            }
            else if ((int)currentAura + 1 > 2)
            {
                currentAura = 0;
            }
            ChangeAllEssences();
        }
    }

    public void changeLoadoutBack(InputAction.CallbackContext context)
    {
        if (context.performed && !GameManager.Instance.stopPlayer)
        {
            if (spiritAbilityHolder.essenceActive)
            {
                spiritAbilityHolder.essenceActive = false;
                spiritAbilityHolder.spiritEssence.Deactivate(this.gameObject);
            }

            if (currentAura - 1 == Aura.Green && GameManager.Instance.green)
            {
                currentAura -= 1;
            }
            else if (currentAura - 1 == Aura.Blue && GameManager.Instance.blue)
            {
                currentAura -= 1;
            }
            else if ((int)currentAura - 1 < 0 && GameManager.Instance.red)
            {
                currentAura += 2;
            }
            else if ((int)currentAura - 1 < 0 && GameManager.Instance.green)
            {
                currentAura += 1;
            }
            ChangeAllEssences();
        }
    }

    public void ChangeSpiritEssence(InventoryEssenceBtn _btn)
    {
        switch (currentAura)
        {
            case Aura.Blue:
                if (blueSpiritEssence == null && _btn.quantity > 0)
                {
                    blueSpiritEssence = Resources.Load<SpiritEssence>(_btn.path);
                    spiritAbilityHolder.spiritEssence = blueSpiritEssence;
                    _btn.blueEquipImg.SetActive(true);
                    _btn.quantity--;
                    _btn.quantityText.text = _btn.quantity.ToString();
                }
                break;
            case Aura.Green:
                if (greenSpiritEssence == null && _btn.quantity > 0)
                {
                    greenSpiritEssence = Resources.Load<SpiritEssence>(_btn.path);
                    spiritAbilityHolder.spiritEssence = greenSpiritEssence;
                    _btn.greenEquipImg.SetActive(true);
                    _btn.quantity--;
                    _btn.quantityText.text = _btn.quantity.ToString();
                }
                break;
            case Aura.Red:
                if (redSpiritEssence == null && _btn.quantity > 0)
                {
                    redSpiritEssence = Resources.Load<SpiritEssence>(_btn.path);
                    spiritAbilityHolder.spiritEssence = redSpiritEssence;
                    _btn.redEquipImg.SetActive(true);
                    _btn.quantity--;
                    _btn.quantityText.text = _btn.quantity.ToString();
                }
                break;
        }
    }

    public void UnquipSpiritEssence(InventoryEssenceBtn _btn)
    {
        switch (currentAura)
        {
            case Aura.Blue:
                if (blueSpiritEssence != null)
                {
                    if (blueSpiritEssence.name == _btn.name)
                    {
                        blueSpiritEssence = null;
                        spiritAbilityHolder.spiritEssence = null;
                        _btn.blueEquipImg.SetActive(false);
                        _btn.quantity++;
                        _btn.quantityText.text = _btn.quantity.ToString();
                    }
                }
                break;
            case Aura.Green:
                if (greenSpiritEssence != null)
                {
                    if (greenSpiritEssence.name == _btn.name)
                    {
                        greenSpiritEssence = null;
                        spiritAbilityHolder.spiritEssence = null;
                        _btn.greenEquipImg.SetActive(false);
                        _btn.quantity++;
                        _btn.quantityText.text = _btn.quantity.ToString();
                    }
                }
                break;
            case Aura.Red:
                if (redSpiritEssence != null)
                {
                    if (redSpiritEssence.name == _btn.name)
                    {
                        redSpiritEssence = null;
                        spiritAbilityHolder.spiritEssence = null;
                        _btn.redEquipImg.SetActive(false);
                        _btn.quantity++;
                        _btn.quantityText.text = _btn.quantity.ToString();
                    }
                }
                break;
        }
    }

    public void ChangeCombatEssence(InventoryEssenceBtn _btn)
    {
        switch (currentAura)
        {
            case Aura.Blue:
                if (blueCombatEssence == null && _btn.quantity > 0)
                {
                    blueCombatEssence = Resources.Load<CombatEssence>(_btn.path);
                    combatAbilityHolder.combatEssence = blueCombatEssence;
                    _btn.blueEquipImg.SetActive(true);
                    _btn.quantity--;
                    _btn.quantityText.text = _btn.quantity.ToString();
                }
                break;
            case Aura.Green:
                if (greenCombatEssence == null && _btn.quantity > 0)
                {
                    greenCombatEssence = Resources.Load<CombatEssence>(_btn.path);
                    combatAbilityHolder.combatEssence = greenCombatEssence;
                    _btn.greenEquipImg.SetActive(true);
                    _btn.quantity--;
                    _btn.quantityText.text = _btn.quantity.ToString();
                }
                break;
            case Aura.Red:
                if (redCombatEssence == null && _btn.quantity > 0)
                {
                    redCombatEssence = Resources.Load<CombatEssence>(_btn.path);
                    combatAbilityHolder.combatEssence = redCombatEssence;
                    _btn.redEquipImg.SetActive(true);
                    _btn.quantity--;
                    _btn.quantityText.text = _btn.quantity.ToString();
                }
                break;
        }
    }

    public void UnquipCombatEssence(InventoryEssenceBtn _btn)
    {
        switch (currentAura)
        {
            case Aura.Blue:
                if (blueCombatEssence != null)
                {
                    if (blueCombatEssence.name == _btn.name)
                    {
                        blueCombatEssence = null;
                        combatAbilityHolder.combatEssence = null;
                        _btn.blueEquipImg.SetActive(false);
                        _btn.quantity++;
                        _btn.quantityText.text = _btn.quantity.ToString();
                    }
                }
                break;
            case Aura.Green:
                if (greenCombatEssence != null)
                {
                    if (greenCombatEssence.name == _btn.name)
                    {
                        greenCombatEssence = null;
                        combatAbilityHolder.combatEssence = null;
                        _btn.greenEquipImg.SetActive(false);
                        _btn.quantity++;
                        _btn.quantityText.text = _btn.quantity.ToString();
                    }
                }
                break;
            case Aura.Red:
                if (redCombatEssence != null)
                {
                    if (redCombatEssence.name == _btn.name)
                    {
                        redCombatEssence = null;
                        combatAbilityHolder.combatEssence = null;
                        _btn.redEquipImg.SetActive(false);
                        _btn.quantity++;
                        _btn.quantityText.text = _btn.quantity.ToString();
                    }
                }
                break;
        }
    }

    public void ChangePassiveEssences(InventoryEssenceBtn _btn)
    {
        switch (currentAura)
        {
            case Aura.Blue:
                if (bluePassiveEssence.Count < 3 && _btn.quantity > 0)
                {
                    bluePassiveEssence.Add(Resources.Load<PassiveEssence>(_btn.path));
                    passiveAbilityHolder.passive.Add(Resources.Load<PassiveEssence>(_btn.path));
                    _btn.blueEquipImg.SetActive(true);
                    _btn.quantity--;
                    _btn.quantityText.text = _btn.quantity.ToString();
                }
                break;
            case Aura.Green:
                if (greenPassiveEssence.Count < 3 && _btn.quantity > 0)
                {
                    greenPassiveEssence.Add(Resources.Load<PassiveEssence>(_btn.path));
                    passiveAbilityHolder.passive.Add(Resources.Load<PassiveEssence>(_btn.path));
                    _btn.greenEquipImg.SetActive(true);
                    _btn.quantity--;
                    _btn.quantityText.text = _btn.quantity.ToString();
                }
                break;
            case Aura.Red:
                if (redPassiveEssence.Count < 3 && _btn.quantity > 0)
                {
                    redPassiveEssence.Add(Resources.Load<PassiveEssence>(_btn.path));
                    passiveAbilityHolder.passive.Add(Resources.Load<PassiveEssence>(_btn.path));
                    _btn.redEquipImg.SetActive(true);
                    _btn.quantity--;
                    _btn.quantityText.text = _btn.quantity.ToString();
                }
                break;
        }
        passiveAbilityHolder.updateStats();
    }

    public void UnequipPassiveEssences(InventoryEssenceBtn _btn)
    {
        switch (currentAura)
        {
            case Aura.Blue:
                bluePassiveEssence.Remove(Resources.Load<PassiveEssence>(_btn.path));
                passiveAbilityHolder.passive.Remove(Resources.Load<PassiveEssence>(_btn.path));
                if(!bluePassiveEssence.Contains(Resources.Load<PassiveEssence>(_btn.path)))
                {
                    _btn.blueEquipImg.SetActive(false);
                }
                _btn.quantity++;
                _btn.quantityText.text = _btn.quantity.ToString();
                break;
            case Aura.Green:
                greenPassiveEssence.Remove(Resources.Load<PassiveEssence>(_btn.path));
                passiveAbilityHolder.passive.Remove(Resources.Load<PassiveEssence>(_btn.path));
                if (!greenPassiveEssence.Contains(Resources.Load<PassiveEssence>(_btn.path)))
                {
                    _btn.greenEquipImg.SetActive(false);
                }
                _btn.quantity++;
                _btn.quantityText.text = _btn.quantity.ToString();
                break;
            case Aura.Red:
                redPassiveEssence.Remove(Resources.Load<PassiveEssence>(_btn.path));
                passiveAbilityHolder.passive.Remove(Resources.Load<PassiveEssence>(_btn.path));
                if (!redPassiveEssence.Contains(Resources.Load<PassiveEssence>(_btn.path)))
                {
                    _btn.redEquipImg.SetActive(false);
                }
                _btn.quantity++;
                _btn.quantityText.text = _btn.quantity.ToString();
                break;
        }
        passiveAbilityHolder.updateStats();
    }

    public void ChangeAllEssences()
    {
        switch (currentAura)
        {
            case Aura.Blue:
                if (blueCombatEssence != null)
                {
                    combatAbilityHolder.combatEssence = blueCombatEssence;
                }
                else
                {
                    combatAbilityHolder.combatEssence = null;
                }

                if (blueSpiritEssence != null)
                {
                    spiritAbilityHolder.spiritEssence = blueSpiritEssence;
                }
                else
                {
                    spiritAbilityHolder.spiritEssence = null;
                }

                passiveAbilityHolder.passive.Clear();
                for (int i = 0; i < bluePassiveEssence.Count; i++)
                {
                    passiveAbilityHolder.passive.Add(bluePassiveEssence[i]);
                }
                break;
            case Aura.Green:
                if (greenCombatEssence != null)
                {
                    combatAbilityHolder.combatEssence = greenCombatEssence;
                }
                else
                {
                    combatAbilityHolder.combatEssence = null;
                }

                if (greenSpiritEssence != null)
                {
                    spiritAbilityHolder.spiritEssence = greenSpiritEssence;
                }
                else
                {
                    spiritAbilityHolder.spiritEssence = null;
                }

                passiveAbilityHolder.passive.Clear();
                for (int i = 0; i < greenPassiveEssence.Count; i++)
                {
                    passiveAbilityHolder.passive.Add(greenPassiveEssence[i]);
                }
                break;
            case Aura.Red:
                if (redCombatEssence != null)
                {
                    combatAbilityHolder.combatEssence = redCombatEssence;
                }
                else
                {
                    combatAbilityHolder.combatEssence = null;
                }

                if (redSpiritEssence != null)
                {
                    spiritAbilityHolder.spiritEssence = redSpiritEssence;
                }
                else
                {
                    spiritAbilityHolder.spiritEssence = null;
                }

                passiveAbilityHolder.passive.Clear();
                for (int i = 0; i < redPassiveEssence.Count; i++)
                {
                    passiveAbilityHolder.passive.Add(redPassiveEssence[i]);
                }
                break;
        }
        passiveAbilityHolder.updateStats();
    }

    public void StartEquipBluePassiveEssences(InventoryEssenceBtn _btn)
    {
        if (bluePassiveEssence.Count < 3 && _btn.quantity > 0)
        {
            bluePassiveEssence.Add(Resources.Load<PassiveEssence>(_btn.path));
            passiveAbilityHolder.passive = bluePassiveEssence;
            _btn.blueEquipImg.SetActive(true);
            _btn.quantity--;
            _btn.quantityText.text = _btn.quantity.ToString();
        }
    }

    public void StartEquipBlueSpiritEssences(InventoryEssenceBtn _btn)
    {
        if (blueSpiritEssence == null && _btn.quantity > 0)
        {
            blueSpiritEssence = Resources.Load<SpiritEssence>(_btn.path);
            spiritAbilityHolder.spiritEssence = blueSpiritEssence;
            _btn.blueEquipImg.SetActive(true);
            _btn.quantity--;
            _btn.quantityText.text = _btn.quantity.ToString();
        }
    }

    public void StartEquipBlueCombatEssences(InventoryEssenceBtn _btn)
    {
        if (blueCombatEssence == null && _btn.quantity > 0)
        {
            blueCombatEssence = Resources.Load<CombatEssence>(_btn.path);
            combatAbilityHolder.combatEssence = blueCombatEssence;
            _btn.blueEquipImg.SetActive(true);
            _btn.quantity--;
            _btn.quantityText.text = _btn.quantity.ToString();
        }
    }

    public void StartEquipGreenPassiveEssences(InventoryEssenceBtn _btn)
    {
        if (greenPassiveEssence.Count < 3 && _btn.quantity > 0)
        {
            greenPassiveEssence.Add(Resources.Load<PassiveEssence>(_btn.path));
            _btn.greenEquipImg.SetActive(true);
            _btn.quantity--;
            _btn.quantityText.text = _btn.quantity.ToString();
        }
    }

    public void StartEquipGreenSpiritEssences(InventoryEssenceBtn _btn)
    {
        if (greenSpiritEssence == null && _btn.quantity > 0)
        {
            greenSpiritEssence = Resources.Load<SpiritEssence>(_btn.path);
            _btn.greenEquipImg.SetActive(true);
            _btn.quantity--;
            _btn.quantityText.text = _btn.quantity.ToString();
        }
    }

    public void StartEquipGreenCombatEssences(InventoryEssenceBtn _btn)
    {
        if (greenCombatEssence == null && _btn.quantity > 0)
        {
            greenCombatEssence = Resources.Load<CombatEssence>(_btn.path);
            _btn.greenEquipImg.SetActive(true);
            _btn.quantity--;
            _btn.quantityText.text = _btn.quantity.ToString();
        }
    }
    public void StartEquipRedPassiveEssences(InventoryEssenceBtn _btn)
    {
        if (redPassiveEssence.Count < 3 && _btn.quantity > 0)
        {
            redPassiveEssence.Add(Resources.Load<PassiveEssence>(_btn.path));
            _btn.redEquipImg.SetActive(true);
            _btn.quantity--;
            _btn.quantityText.text = _btn.quantity.ToString();
        }
    }

    public void StartEquipRedSpiritEssences(InventoryEssenceBtn _btn)
    {
        if (redSpiritEssence == null && _btn.quantity > 0)
        {
            redSpiritEssence = Resources.Load<SpiritEssence>(_btn.path);
            _btn.redEquipImg.SetActive(true);
            _btn.quantity--;
            _btn.quantityText.text = _btn.quantity.ToString();
        }
    }

    public void StartEquipRedCombatEssences(InventoryEssenceBtn _btn)
    {
        if (redCombatEssence == null && _btn.quantity > 0)
        {
            redCombatEssence = Resources.Load<CombatEssence>(_btn.path);
            _btn.redEquipImg.SetActive(true);
            _btn.quantity--;
            _btn.quantityText.text = _btn.quantity.ToString();
        }
    }

}
