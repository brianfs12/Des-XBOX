using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataformas_atrabesables : MonoBehaviour
{
   /*public string colTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("cabeza_player"))
        {
            gameObject.layer = LayerMask.NameToLayer("atravezable");
            StartCoroutine(Ireactivar());
        }
    }

    private IEnumerator Ireactivar()
    {
        yield return new WaitForSeconds(1f);
        gameObject.layer = LayerMask.NameToLayer("floor");
        if (colTag == "Player") {
            gameObject.layer = LayerMask.NameToLayer("atravezable");
            StartCoroutine(Ireactivar());
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        colTag = collision.transform.tag;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        colTag = "";
    }*/

}
