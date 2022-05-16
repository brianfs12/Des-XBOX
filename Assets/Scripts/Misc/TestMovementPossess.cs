using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestMovementPossess : MonoBehaviour
{
    public Transform point; // get the player transform, or w/e object you want to limit in a circle
    public Transform circleCenter; // this is a location that is set to the middle of my world, it will be the center of your circle.
    public float radius = 20f; // this is the range you want the player to move without restriction
    public float speed;
    public Rigidbody2D rb;
    public Vector2 movement;
    private void Awake()
    {
        InputSystem.DisableDevice(Mouse.current);
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float dist = Vector3.Distance(point.position, circleCenter.position); // the distance from player current position to the circleCenter

        if (dist > radius)
        {
            Vector3 fromOrigintoObject = point.position - circleCenter.position;
            fromOrigintoObject *= radius / dist;
            point.position = circleCenter.position + fromOrigintoObject;
            transform.position = point.position;
        }
    }

    private void FixedUpdate()
    {
        if(movement == Vector2.zero)
        {
            rb.MovePosition(circleCenter.position);
        }
        else
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
        
    }

    public void Move(InputAction.CallbackContext context)
    {
            movement = context.ReadValue<Vector2>();
            movement.Normalize();
    }

}
