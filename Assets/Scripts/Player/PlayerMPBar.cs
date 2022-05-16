using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMPBar : MonoBehaviour
{

    Slider slider;
    public Text texto;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = GameManager.Instance.playerStats.currentMP/ GameManager.Instance.playerStats.maxMP;
        texto.text = ((int)GameManager.Instance.playerStats.currentMP).ToString()+" / "+ ((int)GameManager.Instance.playerStats.maxMP).ToString();
    }
}
