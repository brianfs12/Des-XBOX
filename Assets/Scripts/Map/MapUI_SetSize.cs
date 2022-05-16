using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUI_SetSize : MonoBehaviour
{
    void Start()
    {
        gameObject.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta;
    }
}
