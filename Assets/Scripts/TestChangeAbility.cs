using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChangeAbility : MonoBehaviour
{
    public PlayerSpiritAbilityHolder abilityHolder;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            abilityHolder.spiritEssence = Resources.Load<SpiritEssence>("Scriptable Objects/Essences/Fly Essence");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            abilityHolder.spiritEssence = Resources.Load<SpiritEssence>("Scriptable Objects/Essences/Possession Essence");
        }
    }
}
