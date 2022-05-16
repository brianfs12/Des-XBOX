using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckTimeInCollision : MonoBehaviour
{
    public bool playerBehind = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("OneWay"))
        {
            playerBehind = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("OneWay"))
        {
            playerBehind = false;
        }
    }
}
