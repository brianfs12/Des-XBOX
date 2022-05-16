using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private PlayerGroundCheck groundCheck;

    public PlayerFallThrough playerFall;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            playerFall = collision.transform.GetComponent<PlayerFallThrough>();
            groundCheck = collision.transform.GetComponent<PlayerGroundCheck>();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(playerFall == null)
        {
            return;
        }
        if(playerFall.fallThrough)
        {
            effector.rotationalOffset = 180;
            playerFall = null;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        playerFall = null;
        effector.rotationalOffset = 0;
    }
}
