using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSimpleSlopeCheck : MonoBehaviour
{
    [SerializeField]
    private PhysicsMaterial2D noFriction;
    [SerializeField]
    private PhysicsMaterial2D fullFriction;
    [SerializeField]
    private PlayerMovement playerMoveScript;
    [SerializeField]
    private PlayerGroundCheck playerGroundCheck;

    public Transform groundCheck;
    public LayerMask groundLayer;

    private bool onSlope;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMoveScript = GetComponent<PlayerMovement>();
        playerGroundCheck = GetComponent<PlayerGroundCheck>();
    }

    private void FixedUpdate()
    {
        if(onSlope)
        {
            if(playerMoveScript.horizontal.x != 0.0f)
            {
                rb.sharedMaterial = noFriction;
            }
            else
            {
                rb.sharedMaterial = fullFriction;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("slope"))
        {
            onSlope = true;
        }
        if(collision.transform.CompareTag("floor"))
        {
            onSlope = false;
            rb.sharedMaterial = noFriction;
        }
    }

   /* private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("slope"))
        {
            onSlope = false;
            rb.sharedMaterial = noFriction;
        }
    }
   */
}
