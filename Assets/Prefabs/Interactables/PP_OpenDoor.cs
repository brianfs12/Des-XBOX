using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PP_OpenDoor : MonoBehaviour
{
    public MetalCurtains door;
    public bool wasActivated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && door != null)
        {
            if (!wasActivated)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Misc/mechanismActivated", transform.position);
                wasActivated = true;
            }
            door.openCurtain();
        }
    }
}
