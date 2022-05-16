using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Base Stats")]
    public int baseHP;
    public int baseMP;
    public float baseSPD;
    public int baseATK;

    [Header("Applied Stats")]
    public int currentATK;
    public int maxHealth;
    public int currentHealth;
    public int maxMP;
    public int currentMP;
    //public int MPLossOverTime;
    public int MPRecoverPerSecond;
    public float bulletRange;
    public float spiritRange;
    public int extraMaxRange;
    public int teletransportationCost = 0;

    void Start()
    {
        bulletRange = 3;
        spiritRange = 3;
        extraMaxRange = 7;
        //Iniciar si no hay archivo de guardado
        if(baseHP == 0)
        {
            baseHP = 100;
        }
        if (baseMP == 0)
        {
            baseMP = 100;
        }
        if(baseSPD == 0)
        {
            baseSPD = 6;
        }
        if(baseATK == 0)
        {
            baseATK = 10;
        }
        currentATK = baseATK;
        maxHealth = baseHP;
        currentHealth = maxHealth;
        maxMP = baseMP;
        currentMP = maxMP;
    }

    void Update()
    {
        if (currentMP > maxMP) {
            currentMP = maxMP;
        }
        if (currentMP < 0)
        {
            currentMP = 0;
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }
}
