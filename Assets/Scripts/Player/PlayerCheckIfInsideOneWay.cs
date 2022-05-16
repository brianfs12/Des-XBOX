using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckIfInsideOneWay : MonoBehaviour
{
    public bool behindOneWay = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("BehindOneWay"))
        {
            behindOneWay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("BehindOneWay"))
        {
            behindOneWay = false;
        }
    }
}
