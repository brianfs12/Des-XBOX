using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPhaseAnim : MonoBehaviour
{
    public GameObject boss2Chest;
    public GameObject boss2Legs;

    public void Deactivate()
    {
        gameObject.SetActive(false);

        boss2Legs.SetActive(true);
        boss2Chest.SetActive(true);

        GameManager.Instance.ResumePlayer();
        GameManager.Instance.canPause = true;
    }

    public void PlayCutSound(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }
}
