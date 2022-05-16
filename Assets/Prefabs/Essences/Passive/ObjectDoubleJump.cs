using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDoubleJump : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.itemsCollected++; //Añadir 1 al numero de items recolectados pero solo si en nuevo
            GameManager.Instance.doubleJump = true;
            GameManager.Instance.player.GetComponent<PlayerJump>().maxJumpTimes = 2;
            GameManager.Instance.player.GetComponent<PlayerJump>().jumpTimes = 2;
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if(gameObject.active)
        GetComponentInParent<ShowItemText>().StartTextBox();
    }
}
