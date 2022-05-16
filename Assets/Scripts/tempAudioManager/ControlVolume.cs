using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlVolume : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource Mngr;
    // Update is called once per frame
    public void ChangeVolume()
    {
        GetComponent<AudioSource>().volume = volumeSlider.value;
    }
}
