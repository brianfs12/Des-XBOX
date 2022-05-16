using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPopOut : EnemyBase
{
    public int maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    void DeactivateObject()
    {
        //gameObject.SetActive(false);
    }
}
