using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIncreaseHP : MonoBehaviour
{
    public int HPToAdd;
    public int id;
    public bool collected;

    void Start()
    {
        GameManager.Instance.gameProgress.quantityHPIncreaseItems.Add(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.playerStats.maxHealth += HPToAdd;
            collected = true;
            gameObject.SetActive(false);
        }
    }
}
