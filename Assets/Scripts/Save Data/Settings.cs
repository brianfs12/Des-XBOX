using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Settings
{

    /*public bool BgmEnabled;
    public bool SfxEnabled;*/
    public float BgmVolume;
    public float SfxVolume;
    public int currentResolutionWidth;
    public int currentResolutionHeight;
    public int currentResolutionIndex;

    public Settings(DataManager manager)
    {
        //BgmEnabled = manager.BgmEnabled;
        //SfxEnabled = manager.SfxEnabled;
        BgmVolume = manager.BgmVolume;
        SfxVolume = manager.SfxVolume;
        currentResolutionWidth = manager.currentResolutionWidth;
        currentResolutionHeight = manager.currentResolutionHeight;
        currentResolutionIndex = manager.currentResolutionIndex;
    }
}
