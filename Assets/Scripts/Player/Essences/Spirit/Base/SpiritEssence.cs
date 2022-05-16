using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiritEssence : ScriptableObject
{
    public string path;

    public new string name;

    public string description;

    public int mpCostPerSecond;

    public Image hudIcon;

    public GameObject UIButton;

    public virtual void Activate(GameObject parent)
    {
    
    }

    public virtual void Deactivate(GameObject parent)
    {

    }
}
