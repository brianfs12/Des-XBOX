using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Chest_Idle : MonoBehaviour
{
    public Boss_2_Chest_StateMachine stateMachine;
    public float magnitude;
    public float velocity;

    public float idleTime;
    float counter;
    Vector3 startPosition;

    //Resetear cada que inicia el idle
    public float timer;

    void Start()
    {
        counter = idleTime;
        startPosition = transform.position;
    }

    void Update()
    {
        timer += Time.deltaTime;

        counter -= Time.deltaTime;
        Movement();

        if (counter <= 0.0f)
        {
            stateMachine.ActivateState(stateMachine.attack);
        }
    }

    void Movement()
    {
        transform.position = startPosition + new Vector3(0.0f, Mathf.PingPong(timer * velocity, magnitude), 0.0f);
    }

    private void OnEnable()
    {
        counter = idleTime;
        startPosition = transform.position;
    }
}
