//Comenzar el dialogo, ponerlo en cada objeto con el que comenzaras un dialogo
//Aqui se escriben tambian los dialogos de cada conversacion
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool endDialogue;
    DialogueSystem dialogueSystem;

    private void Start()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
    }

    private void Update()
    {
        endDialogue = dialogueSystem.endDialogue;
    }

    public void TriggerDialogue()
    {
        dialogueSystem.StartDialogue(dialogue);
    }

    public void DisplayNextSentence()
    {
        dialogueSystem.ShowNextSentence();
    }
}
