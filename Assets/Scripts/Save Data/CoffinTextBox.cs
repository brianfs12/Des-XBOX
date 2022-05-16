using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffinTextBox : MonoBehaviour
{
    public Animator TextBoxAnim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Interact"))
        {
            TextBoxAnim.SetTrigger("Open");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interact"))
        {
            TextBoxAnim.SetTrigger("Close");
        }
    }
}
