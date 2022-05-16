using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIncreaseBulletRange : MonoBehaviour
{
    public float rangeToAdd;
    public int id;
    public bool collected;

    void Start()
    {
        GameManager.Instance.gameProgress.quantityRangeIncreaseItems.Add(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.playerStats.bulletRange += rangeToAdd;
            GameManager.Instance.playerRange.UpdateRadius();
            collected = true;
            //gameObject.SetActive(false);
        }
    }
}
