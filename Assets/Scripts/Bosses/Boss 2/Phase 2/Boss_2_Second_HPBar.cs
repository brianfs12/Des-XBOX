//Barra de vida para la fase 2 del Boss 2 que se comparte entre las dos partes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_2_Second_HPBar : MonoBehaviour
{
    public Image bossHp;
    public Image bossDrain;
    public float velocidad;
    public Boss_2_Base legs;
    public Boss_2_Base chest;

    int totalHP;

    float tempValue;
    float initValue = 0;
    float t = 0;

    private void Start()
    {
        totalHP = legs.maxHP + chest.maxHP;
        velocidad = 2f;
    }

    void Update()
    {
        float totalCurrentHealth = (float)legs.currentHealth + (float)chest.currentHealth;

        bossHp.fillAmount = totalCurrentHealth / (float)totalHP;
        if (totalCurrentHealth > 0)
        {
            if (bossHp.fillAmount != tempValue)
            {
                t = 0;
                tempValue = bossHp.fillAmount;
                initValue = bossDrain.fillAmount;
            }

            if (t <= 1)
            {
                t += velocidad * 0.001f;
            }

            if (bossDrain.fillAmount > bossHp.fillAmount)
            {
                bossDrain.fillAmount = Mathf.Lerp(initValue, tempValue, t);
            }
            else
            {
                bossDrain.fillAmount = bossHp.fillAmount;
            }

        }
        else
        {
            bossDrain.fillAmount = 0;
        }
    }
}
