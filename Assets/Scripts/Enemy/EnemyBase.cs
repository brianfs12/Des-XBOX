using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Stats")]
    public int currentHealth;
    public int damageToPlayer;
    public int maxHP;
    public bool poseido;
    public GameObject playerGO;
    public Vector2 initPos;
    public PlayerHealth pH;
    public float tiempoDeMuerte = 0.3f;
    public SpriteRenderer renderer;
    public bool poseible;

    [Header("Drop")]
    public GameObject EssenceToDrop;
    public int dropProbability;


    private void Awake()
    {
        playerGO = GameObject.Find("Playeer");
        renderer = playerGO.GetComponent<SpriteRenderer>();
        pH = playerGO.GetComponent<PlayerHealth>();
        if (!gameObject.transform.parent.CompareTag("Enemy"))
        {
            initPos = transform.localPosition;
        }
        else
        {
            initPos = gameObject.transform.parent.localPosition;
        }
        maxHP = currentHealth;
        poseible = true;
    }

    private void OnEnable()
    {
        Respawn();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Invoke("Die", tiempoDeMuerte);
        }
    }

    public void Respawn()
    {
        currentHealth = maxHP;

        if (renderer == null)
        {
            renderer = playerGO.GetComponent<SpriteRenderer>();
        }
        renderer.color = Color.white;
        if (!gameObject.transform.parent.CompareTag("Enemy"))
        {
            gameObject.transform.localPosition = initPos;
        }
        else
        {
            gameObject.transform.parent.localPosition = initPos;
        }
    }

    void Die()
    {
        //gameObject.SetActive(false);
        //Instancear efecto de muerte
        if (transform.parent == null || !transform.parent.CompareTag("Enemy"))
        {
            //Instanciar particulas
            gameObject.SetActive(false);
        }
        else if(transform.parent != null && transform.parent.transform.CompareTag("Enemy"))
        {
            //Instanciar particulas
            gameObject.transform.parent.gameObject.SetActive(false);
        }
        //Generar un numero random al activar el enemigo
        int _r = (int)Random.Range(1, 100);
        
        if (_r <= dropProbability && dropProbability != 0)
        {
            GameObject go = Instantiate(EssenceToDrop, transform.position, Quaternion.identity);
        }

        currentHealth = maxHP;

        FMODUnity.RuntimeManager.PlayOneShot("event:/enemies/mobs/genericEnemyDeath", transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (pH != null)
            {
                if (damageToPlayer != -1)
                {
                    pH.TakeDamage(damageToPlayer, this.gameObject);//hacer da�o al jugador cuando chocan
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth pH = collision.gameObject.GetComponent<PlayerHealth>();
            if(pH != null)
            {
                if (damageToPlayer != -1) {
                    pH.TakeDamage(damageToPlayer, this.gameObject);//hacer da�o al jugador cuando chocan
                }
            }
        }

        if (collision.transform.CompareTag("floor")) {
            poseible = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("floor"))
        {
            poseible = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (poseido) {
            poseido = false;
        }

        if (collision.transform.CompareTag("floor"))
        {
            poseible = true;
        }
    }
}
