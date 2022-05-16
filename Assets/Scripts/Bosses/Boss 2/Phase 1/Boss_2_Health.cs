using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Health : EnemyBase
{
    public int maxHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

}
