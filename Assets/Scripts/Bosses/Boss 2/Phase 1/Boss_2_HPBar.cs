using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_2_HPBar : MonoBehaviour
{
    public Image bossHp;
    public Image bossDrain;
    public float velocidad;
    Boss_2_Base boss;

    float tempValue;
    float initValue = 0;
    float t = 0;

    void Start()
    {
        boss = GetComponent<Boss_2_Base>();
    }

    void Update()
    {
        bossHp.fillAmount = (float)boss.currentHealth / (float)boss.maxHP;
        if (boss.currentHealth > 0)
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
