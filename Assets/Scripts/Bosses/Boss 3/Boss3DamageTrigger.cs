using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3DamageTrigger : MonoBehaviour
{
    public int damageToPlayer;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.transform.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerHealth>())
            {
                other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageToPlayer, this.gameObject);//hacer daño al jugador cuando chocan
            }
            else
            {
                other.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(damageToPlayer, this.gameObject);
            }
            gameObject.SetActive(false);
        }
    }
}