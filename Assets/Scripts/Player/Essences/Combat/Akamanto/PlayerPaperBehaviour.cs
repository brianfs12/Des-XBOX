using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaperBehaviour : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

        }
        else if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            collision.GetComponent<EnemyBase>().TakeDamage(1);
        }
        else if (collision.CompareTag("Boss"))
        {
            Destroy(gameObject);
            collision.transform.parent.GetComponent<Boss1Base>().TakeDamage(1);
        }
    }
}
