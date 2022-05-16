using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Base : MonoBehaviour
{
    [Header("Stats")]
    public int currentHealth;
    public int damageToPlayer;
    public int maxHP;
    public bool poseido;
    public boss1trigger trigger;
    Boss1Sounds sounds;

    private void Start()
    {
        //Destruirse si ya fue derrotado
        if(DataManager.instance.boss1)
        {
            Destroy(gameObject);
        }
        maxHP = currentHealth;
        sounds = GetComponent<Boss1Sounds>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
            //Activar el bool que guarda si ya fue derrotado
            GameManager.Instance.boss1 = true;
        }
    }


    public void Destroy()
    {
        print("matar boss");
        Destroy(this.gameObject);
    }

    void Die()
    {
        trigger.die();
        trigger.metalCurtains.openCurtain();

        GameManager.Instance.green = true; //Desbloquear teleport verde
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageToPlayer, this.gameObject);//hacer daño al jugador cuando chocan
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (poseido)
        {
            poseido = false;
        }
    }
}
