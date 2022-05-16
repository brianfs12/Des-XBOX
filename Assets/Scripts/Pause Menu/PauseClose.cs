using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class PauseClose : MonoBehaviour
{
    [Header("Pause Menu Canvas")]
    public GameObject pause;
    public GameObject map;
    public GameObject settings;
    public GameObject auras;
    public GameObject moveset;
    public PauseCanvas pauseCanvasScript;

    public Button firstSelection;
    public Button passive;
    public GameObject rebindPanel;

    FMOD.Studio.EventInstance cancelSound;
    FMOD.Studio.EventInstance closeSound;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && !rebindPanel.activeInHierarchy)
        {
            if (settings.activeInHierarchy)
            {
                BackToPause();
            }
            else if (auras.activeInHierarchy && passive.IsInteractable())
            {
                BackToPause();
            }
            else if(moveset.activeInHierarchy)
            {
                BackToPause();
            }
            else if(map.activeInHierarchy)
            {
                BackToPause();
            }
            else if(pause.activeInHierarchy)
            {
                PlayCloseEvent();
                pause.SetActive(false);
                GameManager.Instance.GetComponent<PauseGame>().PauseTime();
            }
        }
    }


    public void BackToPause()
    {
        PlayBackEvent();
        pauseCanvasScript.BackToPause();
    }

    public void PlayBackEvent()
    {
        cancelSound = FMODUnity.RuntimeManager.CreateInstance("event:/UI/cancel");
        cancelSound.start();
        cancelSound.release();
    }

    public void PlayCloseEvent()
    {
        closeSound = FMODUnity.RuntimeManager.CreateInstance("event:/UI/closeMenu");
        closeSound.start();
        closeSound.release();
    }
}
