using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Return : MonoBehaviour
{
    public Boss_2_StateMachine stateMachine;
    public GameObject[] positions = new GameObject[3];
    public float speed;
    int randomResult;
    float counter = 0.3f;

    private void Start()
    {
        
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, positions[randomResult].transform.position) < 0.1f)
        {
            counter -= Time.deltaTime;
            if (counter <= 0.0f)
            {
                GetComponent<Boss_2_Idle>().timer = 0.0f;
                stateMachine.ActivateState(stateMachine.idle);
            }
        }
        else
        {
            BackToPosition();
        }
    }

    void BackToPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, positions[randomResult].transform.position, speed * Time.deltaTime);
    }

    private void OnEnable()
    {
        counter = 0.3f;
        randomResult = Random.Range(0, 3);
    }

}
