using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class EnemyFloatsUpDown : EnemyBase
{
    public float floatSpeed;

    Rigidbody2D rBody2D;
    Timeline timeline;

    public enum Direction {Up, Down};
    public Direction startDirection;
    int floatDirection;


    private void Start()
    {
        if(startDirection == Direction.Up)
        {
            floatDirection = 1;
        }
        else if(startDirection == Direction.Down)
        {
            floatDirection = -1;
        }
        rBody2D = GetComponent<Rigidbody2D>();
        timeline = GetComponent<Timeline>();
    }

    private void OnValidate()
    {
        if (startDirection == Direction.Up)
        {
            floatDirection = 1;
        }
        else if (startDirection == Direction.Down)
        {
            floatDirection = -1;
        }
    }

    private void OnEnable()
    {
        if (startDirection == Direction.Up)
        {
            floatDirection = 1;
        }
        else if (startDirection == Direction.Down)
        {
            floatDirection = -1;
        }
    }

    private void Update()
    {
        rBody2D.velocity = new Vector2(rBody2D.velocity.x, (floatSpeed * timeline.timeScale) * floatDirection);
    }

    void FlipDirection()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/enemies/mobs/chochinBounce", transform.position);
        floatDirection *= -1;
        //transform.localScale *= new Vector2(1, -1); //Invertir el sprite dependiendo de la direccion
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("floor") || collision.transform.CompareTag("slope"))
        {
            FlipDirection();
        }
    }
}
