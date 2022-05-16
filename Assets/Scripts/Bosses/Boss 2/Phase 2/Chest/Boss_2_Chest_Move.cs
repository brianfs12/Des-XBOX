using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Chest_Move : MonoBehaviour
{
    public Boss_2_Chest_StateMachine stateMachine;
    public GameObject[] positions = new GameObject[5];
    public float speed;
    int actualPosition = 0;
    int randomResult;

    [HideInInspector]
    public bool death = false;

    float time;
    public AnimationCurve curve;

    void Update()
    {
        if (Vector3.Distance(transform.position, positions[randomResult].transform.position) < 0.1f)
        {
            stateMachine.ActivateState(stateMachine.idle);
        }
        else
        {
            if (!death)
            {
                BackToPosition();
            }
        }
    }

    void BackToPosition()
    {
        time += Time.deltaTime;
        Vector3 pos = Vector3.MoveTowards(transform.position, positions[randomResult].transform.position, time * speed);
        pos.y += curve.Evaluate(time);
        transform.position = pos;
    }

    private void OnEnable()
    {
        randomResult = Random.Range(0, 5);
        int _count = 0;
        while(actualPosition == randomResult && _count < 100)
        {
            randomResult = Random.Range(0, 5);
            _count++;
        }
        if (randomResult != actualPosition)
        {
            actualPosition = randomResult;
        }
        time = 0.0f;
    }

    private void OnDisable()
    {
        time = 0.0f;
    }
}
