using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Idle : MonoBehaviour
{
    public Boss_2_StateMachine stateMachine;
    public float magnitude;

    public float idleTime;
    float counter;
    public float waitStart;

    [HideInInspector]
    public Vector3 startPosition;

    Animator anim;

    public float timer;

    private void Start()
    {
        counter = idleTime;
        startPosition = transform.position;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        counter -= Time.deltaTime;
        if (waitStart >= 0.0f)
        {
            waitStart -= Time.deltaTime;
        }

        if (waitStart <= 0.0f)
        {
            timer += Time.deltaTime;
            Movement();
        }

        if (counter <= 0.0f)
        {
            anim.SetBool("Flying", false);
            stateMachine.ActivateState(stateMachine.attack);
        }
    }

    void Movement()
    {
        transform.position = startPosition + new Vector3(0.0f , Mathf.Sin(timer * magnitude), 0.0f);
        transform.position += new Vector3(Mathf.Sin(timer * 3.5f), 0.0f, 0.0f);
    }

    private void OnEnable()
    {
        counter = idleTime;
        startPosition = transform.position;
    }
}
