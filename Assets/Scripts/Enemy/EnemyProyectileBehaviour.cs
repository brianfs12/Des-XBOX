using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class EnemyProyectileBehaviour : MonoBehaviour
{
    public int damageToPlayer;
    public SpriteRenderer sprite;
    public ParticleSystem particles;
    public Light2D luz;
    public bool homing = false;

    private void Awake()
    {
        //sprite = GetComponentInChildren<SpriteRenderer>();
        particles = GetComponentInChildren<ParticleSystem>();
        luz = GetComponentInChildren<Light2D>();
        if (!homing) {
            Destroy(gameObject, 5f);
        }
    }

    void OnBecameInvisible() //tambien se esta llamando por animation event
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Enemy"))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/enemies/bosses/boss1/itemImpact", transform.position);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("floor")) {
            //Destroy(gameObject);
        }
        
        if (other.transform.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerHealth>())
            {
                other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageToPlayer, this.gameObject);//hacer daño al jugador cuando chocan
            }
            else {
                other.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(damageToPlayer, this.gameObject);
            }
            Destroy(gameObject);
        }
    }

}
