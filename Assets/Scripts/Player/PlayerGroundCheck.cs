using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool isGrounded = false;

    public bool IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
        return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
    }

    public bool isOneWay()
    {
        if(Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer) != false)
        {
            return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer).transform.CompareTag("OneWay");
        }
        else
        {
            return false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, 0.3f);
    }
}
