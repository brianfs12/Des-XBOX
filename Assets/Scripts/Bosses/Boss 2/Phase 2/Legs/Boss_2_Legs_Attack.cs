using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Legs_Attack : MonoBehaviour
{
    public Boss_2_Legs_StateMachine stateMachine;
    public float attackSpeed;
    public bool inRight = true;
    public Rigidbody2D rigi;

    Vector3 startPos;

    public float timeToAttack;
    float counter;

    Animator anim;

    private void OnEnable()
    {
        counter = timeToAttack;
        startPos = transform.position;
    }

    void Start()
    {
        counter = timeToAttack;
        startPos = transform.position;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        counter -= Time.deltaTime;

        if (counter <= 0.0f)
        {
            LungePlayer();
        }
        else
        {
            PrepareAttack();
        }
    }

    void PrepareAttack()
    {
        anim.SetBool("Walk", true);
        if (inRight)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x + 0.3f, transform.localPosition.y, 0.0f), 0.5f * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x - 0.3f, transform.localPosition.y, 0.0f), 0.5f * Time.deltaTime);
        }
    }

    void LungePlayer()
    {
        if (inRight)
        {
            rigi.AddForce(Vector2.left * attackSpeed);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            rigi.AddForce(Vector2.right * attackSpeed);
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish") && counter <= 0.0f)
        {
            //Flip sprite
            inRight = !inRight;
            //GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
            stateMachine.ActivateState(stateMachine.idle);
        }
    }
}
