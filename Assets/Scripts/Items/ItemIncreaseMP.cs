using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIncreaseMP : MonoBehaviour
{
    public int MPToAdd;
    public int id;
    public bool collected;

    void Start()
    {
        GameManager.Instance.gameProgress.quantityMPIncreaseItems.Add(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.playerStats.maxMP += MPToAdd;
            collected = true;
            gameObject.SetActive(false);
        }
    }
}
