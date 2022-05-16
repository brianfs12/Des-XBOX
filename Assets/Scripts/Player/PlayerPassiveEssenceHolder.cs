using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPassiveEssenceHolder : MonoBehaviour
{
    public List<PassiveEssence> passive = new List<PassiveEssence>();

    private PlayerStats playerStats;
    public int hpToSend;
    public int mpToSend;
    public int atkToSend;
    public int spdToSend;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    public void updateStats()
    {
        int tmp;
        tmp = 0;
        for(int i = 0; i < passive.Count; i++)
        {
            if (passive[i] != null)
            {
                if (passive[i].hp)
                {
                    tmp += Mathf.RoundToInt(playerStats.baseHP * passive[i].hpPercentage);
                }
                if (passive[i].mp)
                {
                    tmp += Mathf.RoundToInt(playerStats.baseMP * passive[i].mpPercentage);
                }
                if (passive[i].atk)
                {
                    tmp += Mathf.RoundToInt(playerStats.baseATK * passive[i].atkPercentage);
                }
                if (passive[i].speed)
                {
                    tmp += Mathf.RoundToInt(playerStats.baseSPD * passive[i].hpPercentage);
                }
            }
        }
        hpToSend = tmp;
        playerStats.maxHealth = playerStats.baseHP + hpToSend;
        playerStats.maxMP = playerStats.baseMP + mpToSend;
    }
}
