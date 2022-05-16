using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEventsOtherObjects : MonoBehaviour
{
    public void TogglePause()
    {
        GameManager.Instance.TogglePause();
    }
}
