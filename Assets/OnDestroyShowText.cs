using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyShowText : MonoBehaviour
{
    private void OnDestroy()
    {
        GetComponentInParent<ShowItemText>().StartTextBox();
    }
}
