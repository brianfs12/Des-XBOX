using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFly : MonoBehaviour
{
    private bool isFacingRight = true;
    [HideInInspector]
    public Vector2 movement;
    private Rigidbody2D rb;
    private PlayerCrouch crouch;
    private Animator anim;
    private PlayerJump jump;
    private PlayerSpiritMode spirit;
    private PlayerFlip playerFlip;
    private PlayerCrouch playerCrouch;

    public float speed = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        crouch = GetComponent<PlayerCrouch>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jump = GetComponent<PlayerJump>();
        spirit = GetComponent<PlayerSpiritMode>();
        playerFlip = GetComponent<PlayerFlip>();
        playerCrouch = GetComponent<PlayerCrouch>();
    }

    private void OnDisable()
    {
        movement = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!spirit.spirited)
        {
            if (!playerFlip.isFacingRight && movement.x > 0f)
            {
                playerFlip.Flip();
            }
            else if (playerFlip.isFacingRight && movement.x < 0f)
            {
                playerFlip.Flip();
            }
            if(movement.y < 0)
            {
                playerCrouch.crouchInput = true;
            }
            else
            {
                playerCrouch.crouchInput = false;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    public void Fly(InputAction.CallbackContext context)
    {
        if (!GameManager.Instance.stopPlayer && isActiveAndEnabled)
        {
            movement = context.ReadValue<Vector2>();
            movement.Normalize();
        }
    }
}
