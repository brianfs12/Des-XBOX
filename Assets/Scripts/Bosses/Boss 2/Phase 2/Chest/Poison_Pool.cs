using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison_Pool : MonoBehaviour
{
    public int damageToPlayer;
    Animator anim;
    Collider2D coll;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        //Destroy(gameObject, 5.0f);
        StartCoroutine(DestroyGameObject());
    }

    public void DestroyPool()
    {
        Destroy(gameObject);
    }

    IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(5.0f);
        coll.enabled = false;
        anim.SetTrigger("Death");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageToPlayer,this.gameObject);
        }
    }
}
