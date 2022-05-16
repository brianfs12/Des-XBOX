using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerLightController : MonoBehaviour
{
    public Light2D headLight;
    public Light2D spiritModeLight;

    public void ToggleHeadLight()
    {
        if(headLight.enabled)
        {
            headLight.enabled = false;
        }
        else
        {
            headLight.enabled = true;
        }
        
    }

    public void ToggleSpiritModeLight()
    {
        if(spiritModeLight.enabled)
        {
            spiritModeLight.enabled = false;
        }
        else
        {
            spiritModeLight.enabled = true;
        }
        
    }
}
