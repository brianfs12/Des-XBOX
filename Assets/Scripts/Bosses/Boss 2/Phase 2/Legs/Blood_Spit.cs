using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood_Spit : MonoBehaviour
{

    public int damageToPlayer;

    void Start()
    {
        //Destroy(gameObject, 1.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageToPlayer, this.gameObject);
        }
    }
}
