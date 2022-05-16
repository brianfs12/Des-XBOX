using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodPlayer : MonoBehaviour
{
    PlayerMovement movement;
    FMOD.Studio.EventInstance footsteps;
    FMOD.Studio.EventInstance land;

    void Start()
    {
        movement = GetComponentInParent<PlayerMovement>();
        footsteps = FMODUnity.RuntimeManager.CreateInstance("event:/playerSounds/walk");
        land = FMODUnity.RuntimeManager.CreateInstance("event:/playerSounds/land");
    }

    void PlayFootstepsEvent()
    {
        DefineWalkSound();
        footsteps.start();
        footsteps.release();
    }

    public void PlayJumpSound(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }

    void DefineWalkSound()
    {
        if (movement.isWalkingOnWater)
        {
            footsteps = FMODUnity.RuntimeManager.CreateInstance("event:/playerSounds/splashSteps");
        }
        else
        {
            footsteps = FMODUnity.RuntimeManager.CreateInstance("event:/playerSounds/walk");
        }
    }

    public void PlayLandEvent()
    {
        DefineLandSound();
        land.start();
        land.release();
    }

    void DefineLandSound()
    {
        if (movement.isWalkingOnWater)
        {
            land = FMODUnity.RuntimeManager.CreateInstance("event:/playerSounds/splashLand");
        }
        else
        {
            land = FMODUnity.RuntimeManager.CreateInstance("event:/playerSounds/land");
        }
    }
}
