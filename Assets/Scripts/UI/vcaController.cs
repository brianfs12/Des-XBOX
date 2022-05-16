using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vcaController : MonoBehaviour
{
    private FMOD.Studio.VCA vca;
    public string vcaName;

    private Slider slider;
    private float vcaVolume;

    private void Start()
    {
        vca = FMODUnity.RuntimeManager.GetVCA("vca:/" + vcaName);
        slider = GetComponent<Slider>();
        vca.getVolume(out vcaVolume);
        slider.value = vcaVolume;
    }

    public void setVolume(float volume)
    {
        vca.setVolume(volume);
        vca.getVolume(out vcaVolume);
    }
}
