using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    public bool isFacingRight = true;
    public Transform listener;

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        //Vector3 localScale = transform.localScale;
        //localScale.x *= -1f;
        //transform.localScale = localScale;
        transform.Rotate(0f, 180f, 0f);
        listener.transform.Rotate(0f, -180f, 0f);
    }
}
