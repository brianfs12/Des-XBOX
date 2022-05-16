using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss1HPBar : MonoBehaviour
{

    public Image bossHp;
    public Image bossDrain;
    public float velocidad;
    public Text texto;
    public Boss1Base boss;

    public float tempValue;
    public float initValue = 0;
    public float t = 0;

    // Start is called before the first frame update
    void Start()
    {
        //slider = GetComponentInChildren<Slider>();
        boss = GetComponent<Boss1Base>();
        bossHp = GameObject.Find("BOSS HP BAR").GetComponent<Image>();
        bossDrain = GameObject.Find("BOSS HP DRAIN").GetComponent<Image>();
    }

    // Update is called once per frame
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
