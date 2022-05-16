using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground_detector : MonoBehaviour
{
    private player_controler player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<player_controler>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("floor"))
        {
            player.grounded = true;
            player.anim.SetTrigger("end_jump");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("floor"))
        {
            if (!player.grounded) {
                player.grounded = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("floor"))
        {
            player.grounded = false;
        }
    }
}
