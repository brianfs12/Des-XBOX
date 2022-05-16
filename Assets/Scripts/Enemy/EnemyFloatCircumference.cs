using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class EnemyFloatCircumference : EnemyBase
{
    public int maxHealth;
    public float movementSpeed;
    public float rotationRadius;
    public bool antiClockWise;

    private TimelineChild timeline;
    private float posX, posY, angle = 0f;
    private Vector2 rotationCenter;

    public bool rotateRight;
    public float rotationVelocity;

    private void Start()
    {
        currentHealth = maxHealth;
        timeline = GetComponent<TimelineChild>();
        rotationCenter = transform.position;
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        angle += movementSpeed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * rotationRadius;
        transform.position = rotationCenter + offset;

        if(rotateRight)
        {
            transform.Rotate(Vector3.back, rotationVelocity);
        }
        else
        {
            transform.Rotate(Vector3.forward, rotationVelocity);
        }
    }
}
