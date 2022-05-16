using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_FlipToPlayer : MonoBehaviour
{
    SpriteRenderer sprite;
    GameObject player;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector2 playerPos = (player.transform.position - this.transform.position).normalized;

        if (playerPos.x < 0.0f)
        {
            sprite.flipX = false;
        }
        else if(playerPos.x > 0.0f)
        {
            sprite.flipX = true;
        }
    }
}
