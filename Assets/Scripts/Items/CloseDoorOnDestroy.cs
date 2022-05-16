using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class CloseDoorOnDestroy : MonoBehaviour
{
    public MetalCurtains door;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.itemsCollected++; //Añadir 1 al numero de items recolectados pero solo si es nuevo
            GameManager.Instance.spiritAbility = true;
            GameManager.Instance.playerStats.currentMP = GameManager.Instance.playerStats.maxMP;
            GameManager.Instance.blue = true;

            if (door != null)
                door.closeCurtain();
        }
    }

    private void OnDestroy()
    {
        if(door)
        //GetComponentInParent<MMFeedback>().Play();
        GetComponentInParent<ShowItemText>().StartTextBox();
    }
}
