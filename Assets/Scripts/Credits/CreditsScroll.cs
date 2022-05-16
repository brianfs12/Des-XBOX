using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScroll : MonoBehaviour
{
    public float velocity;
    float timer;
    RectTransform trans;

    void Start()
    {
        timer = 0.0f;
        trans = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (trans.anchoredPosition.y < 1580.0f)
        {
            trans.anchoredPosition = new Vector2(0.0f, timer * velocity);
            //trans.anchoredPosition = Vector2.Lerp(Vector2.zero, new Vector2(0.0f, 740.0f), timer / 30.0f);
            timer += Time.deltaTime;
        }

        if(Input.GetButtonDown("Cancel"))
        {
            LevelLoader.instance.LoadScene("mainMenu");
        }
    }
}
