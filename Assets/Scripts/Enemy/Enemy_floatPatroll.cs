using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class Enemy_floatPatroll : EnemyBase
{    
    public int maxHealth;
    public float movementSpeed;

    [Header("Target")]
    public GameObject[] positionTargets;
    private int indexTarget = 0;
    private Vector3 currentTarget;
    private TimelineChild timeline;

    private void Start()
    {
        currentHealth = maxHealth;
        timeline = GetComponent<TimelineChild>();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
        indexTarget = 0;
    }

    void Update()
    {
        currentTarget = positionTargets[indexTarget].transform.position;

        float stepSpeed = movementSpeed * Time.deltaTime * timeline.parent.timeScale;
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, stepSpeed);

        if (transform.position == currentTarget)
        {
            indexTarget++;
            if (indexTarget == positionTargets.Length)
            {
                indexTarget = 0;
            }
        }
    }

}
