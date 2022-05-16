using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuardAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {

        }
        else if (collision.CompareTag("Enemy"))
        {
            print(collision);
            collision.GetComponent<EnemyBase>().TakeDamage(1);
        }
        else if (collision.CompareTag("Boss"))
        {
            collision.transform.parent.GetComponent<Boss1Base>().TakeDamage(1);
        }
    }
}
