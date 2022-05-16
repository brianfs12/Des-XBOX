using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Animator anim;
    public bool spirited;
    public bool isJumping;
    public bool jumpButtonHeld;

    private float horizontal;
    public float speed = 2f;
    public float jumpingPower = 0.1f;
    public float jumpTime;
    private float jumpTimeCounter;
    private bool isFacingRight = true;

    void Update()
    {
        if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }

        if (jumpButtonHeld && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpingPower;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        //Checks if falling
        if (rb.velocity.y < -1.0f && !IsGrounded())
        {
            anim.Play("jump");
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            anim.SetTrigger("init_jump");
            isJumping = true;
            jumpButtonHeld = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpingPower;
        }
        if (context.canceled)
        {
            isJumping = false;
            jumpButtonHeld = false;
        }
    }

    private bool IsGrounded()
    {
        rb.velocity = Vector2.zero;
        isJumping = false;
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        if(context.performed)
        {
            anim.SetBool("running", true);
        }
        if(context.canceled)
        {
            anim.SetBool("running", false);
        }
    }
}
