using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public Vector2 horizontal;
    private Rigidbody2D rb;
    private PlayerCrouch crouch;
    private Animator anim;
    private PlayerJump jump;
    private PlayerSpiritMode spirit;
    private PlayerSlopeCheck playerSlope;
    private PlayerFlip playerFlip;
    public float rayDistance = 10f;
    public float speed = 0f;
    public bool isWalkingOnWater = false;

    // Start is called before the first frame update
    void Awake()
    {
        crouch = GetComponent<PlayerCrouch>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jump = GetComponent<PlayerJump>();
        spirit = GetComponent<PlayerSpiritMode>();
        playerSlope = GetComponent<PlayerSlopeCheck>();
        playerFlip = GetComponent<PlayerFlip>();
    }

    private void OnDisable()
    {
        horizontal = Vector2.zero;
        anim.SetBool("running", false);
    }

    private void OnEnable()
    {
        if (horizontal.x == 0)
        {
            anim.SetBool("running", false);
        }
        else
        {
            anim.SetBool("running", true);
        }
    }

    void Update()
    {
        if(!GameManager.Instance.stopPlayer)
        {
            if (!spirit.spirited)
            {
                if (!playerFlip.isFacingRight && horizontal.x > 0f)
                {
                    playerFlip.Flip();
                }
                else if (playerFlip.isFacingRight && horizontal.x < 0f)
                {
                    playerFlip.Flip();
                }
            }
            if (horizontal.x == 0)
            {
                anim.SetBool("running", false);
            }
            else
            {
                anim.SetBool("running", true);
            }
        }
    }

    private void FixedUpdate()
    {
        CheckInputs();
        if(!crouch.isCrouching && !playerSlope.isOnSlope)
        {
            rb.velocity = new Vector2(horizontal.x * speed, rb.velocity.y);
        }
        if (!crouch.isCrouching && playerSlope.isOnSlope && !jump.isJumping)
        {
            rb.velocity = new Vector2(speed * playerSlope.slopeNormalPerp.x * -horizontal.x, speed * playerSlope.slopeNormalPerp.y * -horizontal.x);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (isActiveAndEnabled)
        {
            //print(context.ReadValue<Vector2>());
            horizontal = context.ReadValue<Vector2>();
            horizontal.Normalize();
        }
    }

    public void CheckInputs()
    {

    }
}
