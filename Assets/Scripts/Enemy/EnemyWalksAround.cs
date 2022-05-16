using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalksAround : EnemyBase
{
    public int maxHealth;
    public float walkSpeed;

    [Header("Misc")]
    //public float raycastDistance;
    Rigidbody2D rBody2D;
    //SpriteRenderer spriteRenderer;
    int walkDirection = 1;

    private void Start()
    {
        rBody2D = GetComponent<Rigidbody2D>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        rBody2D.velocity = new Vector2(walkSpeed * walkDirection, rBody2D.velocity.y);
    }

    void FlipDirection()
    {
        walkDirection *= -1;
        transform.localScale *= new Vector2(-1,1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("floor") && !collision.CompareTag("Player") && !collision.CompareTag("EnemyAttack") && !collision.CompareTag("EnemyArea"))
        {
            FlipDirection();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("floor"))
            FlipDirection();
    }

}
