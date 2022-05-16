using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    private PlayerCrouch crouch;
    public PlayerGroundCheck groundCheck;
    private Rigidbody2D rb;
    public float jumpTimeCounter;
    private Animator anim;
    private bool jumpButtonHeld;
    private PlayerSpiritMode spirit;
    public PlayerCheckIfInsideOneWay inOneWay;
    public int maxJumpTimes;
    public int jumpTimes = 1;
    public ParticleSystem particle;
    private PlayerSlopeCheck playerSlope;
    public FmodPlayer audioPlayer;

    public bool isJumping;
    public float jumpingPower = 0.1f;
    public float jumpTime;
    public bool landed = false;
    public bool cancelCrouch = false;
    public bool doubleJumpFall;

    private float coyoteTime = 0.15f;
    public float coyoteTimeCounter;
    public LayerMask whatIsGround;
 
    void Awake()
    {
        crouch = GetComponent<PlayerCrouch>();
        anim = GetComponent<Animator>();
        groundCheck = GetComponent<PlayerGroundCheck>();
        rb = GetComponent<Rigidbody2D>();
        spirit = GetComponent<PlayerSpiritMode>();
        playerSlope = GetComponent<PlayerSlopeCheck>();
        audioPlayer = GetComponent<FmodPlayer>();
    }

    private void OnDisable()
    {
        //jumpButtonHeld = false;
        coyoteTimeCounter = 0f;
        jumpTimeCounter = 0f;
        rb.velocity = new Vector2(rb.velocity.x, 0);
    }

    public void FixedUpdate()
    {
        //print(jumpTimeCounter);
        //print(isJumping);
        if(rb.velocity.y < 0 && !GameManager.Instance.stopPlayer && !spirit.spirited)
        {
            anim.SetBool("falling", true);
            isJumping = false;
        }
        if (isJumping) {
            if (jumpButtonHeld)
            {
                if (jumpTimeCounter > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                    jumpTimeCounter -= Time.unscaledDeltaTime*(1/Time.timeScale);
                }
                else
                {
                    isJumping = false;
                }
            }
        }
        if (groundCheck.IsGrounded())
        {
            if(inOneWay.behindOneWay == false && !isJumping)
            {
                anim.SetBool("falling", false);
                if (!landed && !spirit.spirited)
                {
                    anim.SetTrigger("end_jump");
                    Land();
                    jumpTimes = maxJumpTimes;
                    //isJumping = false;
                    if (rb.velocity.y < 1.0f)
                    audioPlayer.PlayLandEvent();
                }
            }
        }
        else 
        {
            if(coyoteTimeCounter <= 0f && !doubleJumpFall)
            {
                if(maxJumpTimes > 1)
                {
                    if (jumpTimes > 0)
                    {
                        jumpTimes = 1;
                    }
                    doubleJumpFall = true;
                }
                else
                {
                    jumpTimes = 0;
                }

            }
            coyoteTimeCounter -= Time.deltaTime;
            if (!spirit.spirited) {
                //anim.SetTrigger("init_jump");
            }
            landed = false;
            GameManager.Instance.playerShoot.isJumpig = true;
        }
    }

    public void Land() {
        landed = true;
        doubleJumpFall = false;
        coyoteTimeCounter = coyoteTime;
        GameManager.Instance.playerShoot.isJumpig = false;
    }

    public void DisLand() {
        /*jumpTimes = 0;
        landed = false;
        doubleJumpFall = true;
        coyoteTimeCounter = 0;
        GameManager.Instance.playerShoot.isJumpig = true;*/
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(isActiveAndEnabled)
        {
            if (context.performed && (coyoteTimeCounter > 0f || jumpTimes > 0) && !spirit.spirited && !GameManager.Instance.stopPlayer && spirit.T >= spirit.spiritCoolDown)
            {
                if (groundCheck.isOneWay() && crouch.isCrouching)
                {
                    cancelCrouch = true;
                    //Do Nothing
                }
                else
                {
                    jumpTimes -= 1;
                    if (jumpTimes == 0)
                    {
                        particle.Play();
                        if(maxJumpTimes > 1)
                        FMODUnity.RuntimeManager.PlayOneShot("event:/playerSounds/doubleJump", transform.position);
                    }
                    isJumping = true;
                    jumpButtonHeld = true;
                    //landed = false;
                    jumpTimeCounter = jumpTime;
                    rb.velocity = new Vector2(rb.velocity.x, jumpingPower / Time.deltaTime * Time.unscaledDeltaTime);
                    anim.SetTrigger("init_jump");
                }
            }
            if (context.canceled)
            {
                jumpButtonHeld = false;
                coyoteTimeCounter = 0f;
            }
        }
    }
}
