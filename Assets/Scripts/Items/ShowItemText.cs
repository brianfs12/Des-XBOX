using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowItemText : MonoBehaviour
{

    public Animator textBox;
    public TextMeshProUGUI text;
    public float Text_time = 1f;

    [TextArea]
    public string textToShow;

    public void StartTextBox()
    {
        text.text = textToShow;
        StartCoroutine(ShowTextBox());
    }

    IEnumerator ShowTextBox()
    {
        textBox.SetBool("IsOpen", true);
        yield return new WaitForSeconds(Text_time);
        textBox.SetBool("IsOpen", false);
    }
}
