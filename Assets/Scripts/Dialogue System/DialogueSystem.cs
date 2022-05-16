//Colocar este script en un GameObject DialogueManager
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public bool endDialogue; //Saber si el dialogo terminó para volver a empezar

    public Animator anim; //Animator del cuadro de dialogo, que entra al comenzar y sale de pantalla al terminar el dialogo

    public Queue<string> names; //Lista de nombres
    public Queue<string> sentences; //Lista de dialogos

    public MonoBehaviour[] playerScripts;

    void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
    }


    public void StartDialogue(Dialogue _dialogue)
    {
        endDialogue = false;

        for (int i = 0; i < playerScripts.Length; i++)
        {
            playerScripts[i].enabled = false;
        }

        GameManager.Instance.StopPlayer();

        anim.SetBool("IsOpen", true);

        nameText.text = _dialogue.names[0];

        sentences.Clear();
        names.Clear();


        foreach(string sentence in _dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        foreach (string name in _dialogue.names)
        {
            names.Enqueue(name);
        }

        ShowNextSentence();
    }

    public void ShowNextSentence()
    {

        //Saber si hay mas dialogos
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        string name = names.Dequeue();
        //Detener las coroutines antes de comenzar la otra, para poder saltar dialogos
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence, name));
    }

    //Agrear la oracion letra por letra
    IEnumerator TypeSentence(string sentence, string name)
    {
        dialogueText.text = "";

        nameText.text = name;

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null; //Esperar un frame antes de la siguiente letra
        }
    }

    void EndDialogue()
    {
        anim.SetBool("IsOpen", false);
        endDialogue = true;
        dialogueText.text = "";
        nameText.text = "";

        for (int i = 0; i < playerScripts.Length; i++)
        {
            playerScripts[i].enabled = true;
        }

        GameManager.Instance.ResumePlayer();
    }
}
