using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FlyEssence : SpiritEssence
{
    public override void Activate(GameObject parent)
    {
        Debug.Log("Fly active!");
        PlayerCrouch playerCrouch = parent.GetComponent<PlayerCrouch>();
        PlayerJump playerJump = parent.GetComponent<PlayerJump>();
        PlayerMovement playerMove = parent.GetComponent<PlayerMovement>();
        PlayerFly playerFly = parent.GetComponent<PlayerFly>();
        Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
        parent.GetComponent<Animator>().SetBool("flying", true);
        rb.gravityScale = 0;
        playerFly.movement = playerMove.horizontal;
        playerCrouch.enabled = false;
        playerJump.enabled = false;
        playerMove.enabled = false;
        playerFly.enabled = true;
        FMODUnity.RuntimeManager.PlayOneShot("event:/playerSounds/playerFly", parent.transform.position);
    }

    public override void Deactivate(GameObject parent)
    {
        Debug.Log("Fly deactivate!");
        PlayerCrouch playerCrouch = parent.GetComponent<PlayerCrouch>();
        PlayerJump playerJump = parent.GetComponent<PlayerJump>();
        PlayerMovement playerMove = parent.GetComponent<PlayerMovement>();
        PlayerFly playerFly = parent.GetComponent<PlayerFly>();
        Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
        parent.GetComponent<Animator>().SetBool("flying", false);
        rb.gravityScale = 5;
        playerMove.horizontal = playerFly.movement;
        playerFly.enabled = false;
        playerCrouch.enabled = true;
        playerMove.enabled = true;
        playerJump.enabled = true;
    }
}
