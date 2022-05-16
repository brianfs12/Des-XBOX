using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Legs_Fall : MonoBehaviour
{
    public Boss_2_Legs_StateMachine stateMachine;
    public float waitTime;
    float counter;

    void Start()
    {
        counter = waitTime;
    }

    private void Update()
    {
        counter -= Time.deltaTime;

        if (counter <= 0.0f)
        {
            stateMachine.ActivateState(stateMachine.idle);
        }
    }
}
