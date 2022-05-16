using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatEssence : ScriptableObject
{
    public string path;

    public new string name;

    public string description;

    public int mpCostPerUse;

    public Image hudIcon;

    public GameObject objectToInstantiate;

    public GameObject UIButton;

    public virtual void Activate(GameObject parent)
    {

    }
}
