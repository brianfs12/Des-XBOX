using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlopeCheck : MonoBehaviour
{
    [SerializeField]
    private float slopeCheckDistanceX;
    [SerializeField]
    private float slopeCheckDistanceY;
    [SerializeField]
    private float maxSlopeAngle;
    [SerializeField]
    private PhysicsMaterial2D noFriction;
    [SerializeField]
    private PhysicsMaterial2D fullFriction;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private PlayerMovement playerMoveScript;
    public PlayerCrouch crouch;

    private PlayerJump playerJump;

    private float slopeDownAngle;
    private float slopeSideAngle;
    private float lastSlopeAngle;

    public bool isOnSlope;
    public bool canWalkOnSlope;

    private Vector2 boxColliderSize;
    private Vector2 crouchBoxColliderSize;

    public Vector2 slopeNormalPerp;

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    public BoxCollider2D crouchbc;

    // Start is called before the first frame update
    void Start()
    {
        crouch = GetComponent<PlayerCrouch>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        playerMoveScript = GetComponent<PlayerMovement>();
        crouchbc = crouch.crouch_col;
        playerJump = GetComponent<PlayerJump>();

        boxColliderSize = bc.size;
        crouchBoxColliderSize = crouchbc.size;
    }

    private void FixedUpdate()
    {
        SlopeCheck();
    }

    private void SlopeCheck()
    {
        if(!crouch.isCrouching)
        {
            Vector2 checkPos = transform.position - (Vector3)(new Vector2(0.0f, boxColliderSize.y / 2));
            SlopeCheckHorizontal(checkPos - new Vector2(0.0f, 0.54f));
            SlopeCheckVertical(checkPos);
        }
        else
        {
            Vector2 checkPos = transform.position - (Vector3)(new Vector2(0.0f, crouchBoxColliderSize.y / 2));
            SlopeCheckHorizontal(checkPos - new Vector2(0.0f, 0.54f));
            SlopeCheckVertical(checkPos);
        }


    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistanceX, whatIsGround);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistanceX, whatIsGround);

        if (slopeHitFront)
        {
            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
            if (slopeSideAngle < maxSlopeAngle && !playerJump.isJumping)
            {
                isOnSlope = true;
            }

        }
        else if (slopeHitBack)
        {
            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);

            if(slopeSideAngle < maxSlopeAngle && !playerJump.isJumping)
            {
                isOnSlope = true;
            }
            
        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }


        Debug.DrawRay(checkPos, transform.right / 2, Color.cyan);
        Debug.DrawRay(slopeHitFront.point, slopeHitFront.normal, Color.red);

    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistanceY, whatIsGround);
        
        if (hit)
        {

            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeDownAngle != lastSlopeAngle && !playerJump.isJumping)
            {
                isOnSlope = true;
            }

            lastSlopeAngle = slopeDownAngle;

            Debug.DrawRay(hit.point, slopeNormalPerp, Color.blue);
            Debug.DrawRay(hit.point, hit.normal, Color.green);

        }

        if (slopeDownAngle > maxSlopeAngle || slopeSideAngle > maxSlopeAngle)
        {
            canWalkOnSlope = false;
        }
        else
        {
            canWalkOnSlope = true;
        }
        if (isOnSlope && canWalkOnSlope && playerMoveScript.horizontal.x == 0.0f)
        {
            rb.sharedMaterial = fullFriction;
        }
        else
        {
            rb.sharedMaterial = noFriction;
        }
    }
}
