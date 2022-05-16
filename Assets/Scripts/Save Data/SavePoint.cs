using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public float playerPositionX = 0.0f;
    public float playerPositionY = 0.0f;
    public string areaName;

    public Animator textAnim;

    /*public int maxMP;
    public float bulletRange;
    public float spiritRange;*/

    public void SaveData()
    {
        textAnim.SetTrigger("save");
        //DataManager.instance.maxHealth = GameManager.Instance.playerStats.maxHealth;
        DataManager.instance.playerPositionX = playerPositionX;
        DataManager.instance.playerPositionY = playerPositionY;
        DataManager.instance.areaName = areaName;
        DataManager.instance.time = GameManager.Instance.time;
        /*DataManager.instance.maxMP = maxMP;
        DataManager.instance.bulletRange = bulletRange;
        DataManager.instance.spiritRange = spiritRange;*/
        GameManager.Instance.gameProgress.GuardarListas();

        for (int i = 0; i < GameManager.Instance.gameProgress.passiveEssences.Count; i++)
        {
            DataManager.instance.inventoryPassive.Add(GameManager.Instance.gameProgress.passiveEssences[i].name);
        }
        for (int i = 0; i < GameManager.Instance.gameProgress.combatEssences.Count; i++)
        {
            DataManager.instance.inventoryCombat.Add(GameManager.Instance.gameProgress.combatEssences[i].name);
        }
        for (int i = 0; i < GameManager.Instance.gameProgress.spiritEssences.Count; i++)
        {
            DataManager.instance.inventorySpirit.Add(GameManager.Instance.gameProgress.spiritEssences[i].name);
        }

        if (GameManager.Instance.playerLoadouts.blueCombatEssence != null)
        {
            DataManager.instance.blueCombatEssence = GameManager.Instance.playerLoadouts.blueCombatEssence.name;
        }
        if (GameManager.Instance.playerLoadouts.blueSpiritEssence != null)
        {
            DataManager.instance.blueSpiritEssence = GameManager.Instance.playerLoadouts.blueSpiritEssence.name;
        }
        for (int i = 0; i < GameManager.Instance.playerLoadouts.bluePassiveEssence.Count; i++)
        {
            DataManager.instance.bluePassiveEssence.Add(GameManager.Instance.playerLoadouts.bluePassiveEssence[i].name);
        }

        if (GameManager.Instance.playerLoadouts.greenCombatEssence != null)
        {
            DataManager.instance.greenCombatEssence = GameManager.Instance.playerLoadouts.greenCombatEssence.name;
        }
        if (GameManager.Instance.playerLoadouts.greenSpiritEssence != null)
        {
            DataManager.instance.greenSpiritEssence = GameManager.Instance.playerLoadouts.greenSpiritEssence.name;
        }
        for (int i = 0; i < GameManager.Instance.playerLoadouts.greenPassiveEssence.Count; i++)
        {
            DataManager.instance.greenPassiveEssence.Add(GameManager.Instance.playerLoadouts.greenPassiveEssence[i].name);
        }

        if (GameManager.Instance.playerLoadouts.redCombatEssence != null)
        {
            DataManager.instance.redCombatEssence = GameManager.Instance.playerLoadouts.redCombatEssence.name;
        }
        if (GameManager.Instance.playerLoadouts.redSpiritEssence != null)
        {
            DataManager.instance.redSpiritEssence = GameManager.Instance.playerLoadouts.redSpiritEssence.name;
        }
        for (int i = 0; i < GameManager.Instance.playerLoadouts.redPassiveEssence.Count; i++)
        {
            DataManager.instance.redPassiveEssence.Add(GameManager.Instance.playerLoadouts.redPassiveEssence[i].name);
        }

        DataManager.instance.itemsCollected = GameManager.Instance.itemsCollected;
        DataManager.instance.spiritAbility = GameManager.Instance.spiritAbility;
        DataManager.instance.doubleJump = GameManager.Instance.doubleJump;
        DataManager.instance.boss1 = GameManager.Instance.boss1;
        DataManager.instance.boss2 = GameManager.Instance.boss2;
        DataManager.instance.boss3 = GameManager.Instance.boss3;
        DataManager.instance.blue = GameManager.Instance.blue;
        DataManager.instance.green = GameManager.Instance.green;
        DataManager.instance.red = GameManager.Instance.red;

        DataManager.instance.SaveData(DataManager.saveSlot);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Misc/saveGame", transform.position);

    }

}
