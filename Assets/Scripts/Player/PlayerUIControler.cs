using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIControler : MonoBehaviour
{
    private PlayerStats player;
    public SlicedFilledImage uIHp;
    public SlicedFilledImage uIHpDrain;
    public SlicedFilledImage uISp;
    public SlicedFilledImage uISpDrain;

    public RectTransform uIHpRT;
    public RectTransform uIHpDrainRT;
    public RectTransform uISpRT;
    public RectTransform uISpDrainRT;
    public RectTransform uIHpBarEmptyRT;
    public RectTransform uISpBarEmptyRT;

    public Image uIHpBarEmptyImg;
    public Image uISpBarEmptyImg;

    private float tempValueHp;
    private float initValueHP = 0;
    private float tHp = 0;
    private float tempValueSp;
    private float initValueSP = 0;
    private float tSp = 0;
    private float timerTillDrainStartsMoving;
    public float velocidad = 2;
    public float timeTillDrainStartsMoving;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<PlayerStats>();
        timerTillDrainStartsMoving = timeTillDrainStartsMoving;
    }

    // Update is called once per frame
    void Update()
    {
        //UI hp
        uIHpBarEmptyRT.sizeDelta = new Vector2(Mathf.CeilToInt((float)(player.maxHealth * uIHpBarEmptyImg.mainTexture.width) / 100), uIHpBarEmptyImg.mainTexture.height);
        uIHpDrainRT.sizeDelta = new Vector2(uIHpBarEmptyRT.sizeDelta.x - 2, uIHpDrain.mainTexture.height);
        uIHpRT.sizeDelta = new Vector2(uIHpBarEmptyRT.sizeDelta.x - 2, uIHp.mainTexture.height);
        uIHp.fillAmount = (float)player.currentHealth / (float)player.maxHealth;
        if (player.currentHealth > 0)
        {
            if (uIHp.fillAmount != tempValueHp)
            {
                tHp = 0;
                tempValueHp = uIHp.fillAmount;
                initValueHP = uIHpDrain.fillAmount;
            }

            if (tHp <= 1)
            {
                tHp += velocidad * 0.001f;
            }

            if (uIHpDrain.fillAmount > uIHp.fillAmount)
            {
                uIHpDrain.fillAmount = Mathf.Lerp(initValueHP, tempValueHp, tHp);
            }
            else
            {
                uIHpDrain.fillAmount = uIHp.fillAmount;
            }

        }
        else
        {
            uIHpDrain.fillAmount = 0;
        }

        //UI sp
        uISpBarEmptyRT.sizeDelta = new Vector2(Mathf.CeilToInt((float)(player.maxMP * uISpBarEmptyImg.mainTexture.width) / 100), uISpBarEmptyImg.mainTexture.height);
        uISpDrainRT.sizeDelta = new Vector2(uISpBarEmptyRT.sizeDelta.x - 1, uISpDrain.mainTexture.height);
        uISpRT.sizeDelta = new Vector2(uISpBarEmptyRT.sizeDelta.x - 1, uISp.mainTexture.height);
        uISp.fillAmount = (float)player.currentMP / (float)player.maxMP;
        if (player.currentHealth > 0)
        {
            if (uISp.fillAmount != tempValueSp)
            {
                tSp = 0;
                tempValueSp = uISp.fillAmount;
                initValueSP = uISpDrain.fillAmount;
            }

            if (tSp <= 1)
            {
                tSp += velocidad * 0.001f;
            }

            if (uISpDrain.fillAmount > uISp.fillAmount) {
                uISpDrain.fillAmount = Mathf.Lerp(initValueSP, tempValueSp, tSp);
            }
            else{
                uISpDrain.fillAmount = uISp.fillAmount;
            }

        }
        else {
            uISpDrain.fillAmount = 0;
        }

    }
}
