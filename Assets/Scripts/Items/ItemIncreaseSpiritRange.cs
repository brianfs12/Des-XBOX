using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIncreaseSpiritRange : MonoBehaviour
{
    public float rangeToAdd;
    public int id;
    public bool collected;

    void Start()
    {
        GameManager.Instance.gameProgress.quantitySPRangeIncreaseItems.Add(this);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.playerStats.spiritRange += rangeToAdd;
            GameManager.Instance.playerSPRange.UpdateRadius();
            collected = true;
            gameObject.SetActive(false);
        }
    }
}
