using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAuraColor : MonoBehaviour
{
    Image img;
    public Sprite blue, green, red;

    private void Start()
    {
        img = GetComponent<Image>();
    }

    void Update()
    {
        if(GameManager.Instance.playerLoadouts.currentAura == PlayerAuraLoadouts.Aura.Blue)
        {
            img. sprite = blue;
        }
        else if (GameManager.Instance.playerLoadouts.currentAura == PlayerAuraLoadouts.Aura.Green)
        {
            img.sprite = green;
        }
        else if (GameManager.Instance.playerLoadouts.currentAura == PlayerAuraLoadouts.Aura.Red)
        {
            img.sprite = red;
        }
    }
}
