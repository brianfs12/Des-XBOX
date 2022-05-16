using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpiritEnergy : MonoBehaviour
{
    private PlayerSpiritAbilityHolder spiritHolder;
    private PlayerUIControler uiControl;

    public bool refill = false;
    public int mpCompare;
    public float timeTillDrainStartsMoving;
    public float mpRestorePerTime;
    public float mpCostPerTime;

    private float timerRest1;
    private float timerTillDrainStartsMoving;
    private float timerAdd1;


    private void Awake()
    {
        spiritHolder = GetComponent<PlayerSpiritAbilityHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        if(spiritHolder.spiritEssence && mpCostPerTime != spiritHolder.spiritEssence.mpCostPerSecond)
        {
            mpCostPerTime = spiritHolder.spiritEssence.mpCostPerSecond;
            timerRest1 = 1 / mpCostPerTime;
        }
        if (mpRestorePerTime != GameManager.Instance.playerStats.MPRecoverPerSecond)
        {
            mpRestorePerTime = GameManager.Instance.playerStats.MPRecoverPerSecond;
            timerAdd1 = 1 / mpRestorePerTime;
        }

        if (spiritHolder.essenceActive && !GameManager.Instance.pause)
        {
            if(timerRest1 > 0)
            {
                timerRest1 -= Time.unscaledDeltaTime;
                //Debug.Log(timerRest1);
            }
            else
            {
                GameManager.Instance.playerStats.currentMP -= 1;
                timerRest1 = 1 / mpCostPerTime;
            }
        }
        else if (!spiritHolder.essenceActive && refill)
        {
            if (timerAdd1 > 0)
            {
                timerAdd1 -= Time.unscaledDeltaTime;
                //Debug.Log(timerAdd1);
            }
            else
            {
                GameManager.Instance.playerStats.currentMP += 1;
                timerAdd1 = 1 / mpRestorePerTime;
            }
            if (GameManager.Instance.playerStats.currentMP == GameManager.Instance.playerStats.maxMP)
            {
                refill = false;
            }
        }
    }
}
