using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playTestSound : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            audioManager.instance.PlaySfx("test1");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            audioManager.instance.PlaySfx("test2");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            audioManager.instance.PlayMusic("music");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            audioManager.instance.PlayMusic("music2");
        }
    }
}
