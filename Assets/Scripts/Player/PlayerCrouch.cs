using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCrouch : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool crouchInput = false;
    public bool isCrouching = false;
    public bool otherInput = false;
    public PlayerGroundCheck ground;
    private Animator anim;
    private PlayerJump jump;
    private PlayerMovement move;
    private PlayerSpiritMode spirit;
    private BoxCollider2D col;
    public BoxCollider2D crouch_col;

    public PhysicsMaterial2D noFriction;
    public PhysicsMaterial2D fullFriction;

    public float cooldown = 0f;

    private void Start()
    {
        ground = GetComponent<PlayerGroundCheck>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jump = GetComponent<PlayerJump>();
        move = GetComponent<PlayerMovement>();
        col = GetComponent<BoxCollider2D>();
        spirit = GetComponent<PlayerSpiritMode>();
    }

    private void Update()
    {
        if(cooldown < 0f)
        {
            if (ground.IsGrounded() && crouchInput && !isCrouching && !anim.GetBool("falling") && !GameManager.Instance.stopPlayer)
            {
                isCrouching = true;
                //rb.velocity = Vector2.zero;
                anim.SetBool("agacharse", true);
                crouch_col.enabled = true;
                col.enabled = false;
                ground.groundCheck.transform.GetComponent<BoxCollider2D>().sharedMaterial = fullFriction;
            }
            if (isCrouching && !crouchInput && !GameManager.Instance.stopPlayer)
            {
                isCrouching = false;
                anim.SetBool("agacharse", false);
                col.enabled = true;
                crouch_col.enabled = false;
                ground.groundCheck.transform.GetComponent<BoxCollider2D>().sharedMaterial = noFriction;
            }
            if (jump.isJumping || jump.cancelCrouch)
            {
                cooldown = 0.1f;
                isCrouching = false;
                col.enabled = true;
                crouch_col.enabled = false;
                ground.groundCheck.transform.GetComponent<BoxCollider2D>().sharedMaterial = noFriction;
                anim.SetBool("agacharse", false);
                jump.cancelCrouch = false;
            }
            if (spirit.spirited)
            {
                cooldown = 0.1f;
                isCrouching = false;
                col.enabled = true;
                crouch_col.enabled = false;
                ground.groundCheck.transform.GetComponent<BoxCollider2D>().sharedMaterial = noFriction;
                anim.SetBool("agacharse", false);
                if (!spirit.spirited)
                {
                    spirit.spirited = true;
                    spirit.SetSpirited();
                }
            }
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if(isActiveAndEnabled)
        {
            if (context.started)
            {
                crouchInput = true;
            }
            if (context.canceled)
            {
                crouchInput = false;
            }
        }
    }

   /* private IEnumerator Iatravesar(GameObject plataforma) {
        yield return new WaitForSeconds(0.13f);
        if (isCrouching) {
            PlatformEffector2D effector = plataforma.gameObject.GetComponent<PlatformEffector2D>();
            effector.rotationalOffset = 180;
            StartCoroutine(Ireactivar(effector));
        }
    }

    private IEnumerator Ireactivar(PlatformEffector2D _effector)
    {
        yield return new WaitForSeconds(0.5f);
        _effector.rotationalOffset = 0;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (isCrouching && collision.transform.CompareTag("atravesar")) {
            StartCoroutine(Iatravesar(collision.gameObject));
        }
    } */
}
