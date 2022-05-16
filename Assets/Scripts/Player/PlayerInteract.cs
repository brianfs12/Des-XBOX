using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    bool canInteract;

    //Dialogos
    bool firstDialogue;
    [SerializeField]
    DialogueTrigger npc;

    //Puntos de guardado
    SavePoint savePoint;
    public GameObject savingText;

    public void Interact(InputAction.CallbackContext context)
    {
        //Iniciar dialogo
        if (context.performed && npc != null && !GameManager.Instance.pause)
        {
            if (npc.endDialogue)
            {
                firstDialogue = true;
            }
            //Desactivar dialogo con trigger despues de terminarlo
            if(npc.endDialogue && npc.gameObject.CompareTag("DialogueTrigger"))
            {
                npc = null;
                canInteract = false;
                //Destroy(npc.gameObject);
            }
            if (context.performed && canInteract && firstDialogue)
            {
                npc.TriggerDialogue();
                firstDialogue = false;
            }
            else if (context.performed && canInteract && !firstDialogue)
            {
                npc.DisplayNextSentence();
            }
        }

        //Punto de guardado
        if (context.performed && savePoint != null && canInteract && !GameManager.Instance.pause)
        {
            savePoint.SaveData();
            GameManager.Instance.ResetHealthMP(); //Restaurar HP y MP
            StartCoroutine(SavingText());//temporal para saber que la partida se guardó
        }
    }

    //temporal para saber que la partida se guardó
    IEnumerator SavingText()
    {
        canInteract = false;
        savingText.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        savingText.SetActive(false);
        canInteract = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Iniciar dialogo con boton
        if (collision.gameObject.CompareTag("NPC"))
        {
            firstDialogue = true;
            canInteract = true;
            npc = collision.gameObject.GetComponent<DialogueTrigger>();
        }

        //Iniciar dialogo con trigger
        if(collision.gameObject.CompareTag("DialogueTrigger"))
        {
            canInteract = true;
            npc = collision.gameObject.GetComponent<DialogueTrigger>();
            npc.TriggerDialogue();
        }

        if (collision.gameObject.CompareTag("SavePoint"))
        {
            canInteract = true;
            savePoint = collision.gameObject.GetComponent<SavePoint>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC") || collision.gameObject.CompareTag("DialogueTrigger"))
        {
            canInteract = false;
            npc = null;
        }

        if (collision.gameObject.CompareTag("SavePoint"))
        {
            canInteract = false;
            savePoint = null;
        }
    }
}
