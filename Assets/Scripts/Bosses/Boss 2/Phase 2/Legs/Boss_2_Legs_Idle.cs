using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Legs_Idle : MonoBehaviour
{
    public Boss_2_Legs_StateMachine stateMachine;
    public float waitTime;
    float counter;

    Animator anim;

    int randomInt;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        counter = waitTime;
    }

    private void OnEnable()
    {
        counter = waitTime;
        randomInt = Random.Range(0,3);
        anim.SetBool("Walk", false);
    }

    void Update()
    {
        counter -= Time.deltaTime;

        if (counter <= 0.0f)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            if (randomInt == 1)
            {
                stateMachine.ActivateState(stateMachine.spit);
            }
            else
            {
                stateMachine.ActivateState(stateMachine.attack);
            }
        }
    }
}
