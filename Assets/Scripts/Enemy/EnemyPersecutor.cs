//Azael Sanchez
//Enemigo que sigue al jugador en linea recta
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class EnemyPersecutor : EnemyBase
{
    [SerializeField]
    public float moveSpeed;

    [SerializeField]
    Animator animator;

    GameObject player;
    Timeline timeline;

    bool activeBehaviour;
    bool horVSver;
    bool leftVSright;
    int directions;
    //0 = Up    1 = Right   2 = Down    3 = Left
    int prevDirections;

    void Start()
    {
        player = GameObject.Find("Playeer");
        animator = GetComponent<Animator>();
        timeline = GetComponent<Timeline>();
    }

    private void OnDisable()
    {
        activeBehaviour = false;
    }

    void Update()
    {
        if(Vector2.Distance(player.transform.position, gameObject.transform.position) <= 5f)
        {
            activeBehaviour = true;
        }
        if(activeBehaviour)
        {
            Movement();
        }
    }

    void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime * timeline.timeScale);
        Vector2 direction = (this.transform.position - player.transform.position).normalized;

        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            horVSver = true;
        }
        else
        {
            horVSver = false;
        }

        prevDirections = directions;

        if(direction.x > 0.0f) //Left
        {
            leftVSright = true;
            if (direction.y > 0.0f) //Down
            {
                //Abajo Izquierda
                if(horVSver)
                {
                    directions = 3;
                }
                else
                {
                    directions = 2;
                }
            }
            else //Up
            {
                //Arriba Izquierda
                if (horVSver)
                {
                    directions = 3;
                }
                else
                {
                    directions = 0;
                }
            }
        }
        else //Right
        {
            leftVSright = false;

            if (direction.y > 0.0f) //Down
            {
                //Abajo Derecha
                if (horVSver)
                {
                    directions = 1;
                }
                else
                {
                    directions = 2;
                }
            }
            else //Up
            {
                //Arriba Derecha
                if (horVSver)
                {
                    directions = 1;
                }
                else
                {
                    directions = 0;
                }
            }
        }

        if(prevDirections != directions)
        {
            animator.SetBool("Up", false);
            animator.SetBool("Left", false);
            animator.SetBool("Down", false);

            if(!leftVSright)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            switch (directions)
            {
                case 0:
                    animator.SetBool("Up", true);
                    break;
                case 1:
                    animator.SetBool("Left", true);
                    break;
                case 2:
                    animator.SetBool("Down", true);
                    break;
                case 3:
                    animator.SetBool("Left", true);
                    break;
            }
        }
    }
}
