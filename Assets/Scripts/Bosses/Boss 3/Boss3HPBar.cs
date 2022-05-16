using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HutongGames.PlayMaker;

public class Boss3HPBar : MonoBehaviour
{

    public Image bossHp;
    public Image bossDrain;
    public float velocidad;
    public Text texto;

    public float tempValue;
    public float initValue = 0;
    public float t = 0;
    public float currentHealth;
    public float maxHP;

    public float dañoRecibido = 1;

    public PlayMakerFSM fsm;

    // Start is called before the first frame update
    void Start()
    {
        //slider = GetComponentInChildren<Slider>();
    }

    public void setHP(float hp) {
        currentHealth = hp;
        maxHP = hp;
    }

    public void decreaseHP(float hp) {
        currentHealth -= hp;
    }

    // Update is called once per frame
    void Update()
    {
        bossHp.fillAmount = (float)currentHealth / (float)maxHP;
        if (currentHealth > 0)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile")) {
            currentHealth -= dañoRecibido;
            fsm.FsmVariables.GetFsmFloat("hpBoss").Value = currentHealth;
        }
    }
}
