using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PassiveEssence : ScriptableObject
{
    public string path;

    public new string name;

    public string description;

    [Header("Stats to be affected")]
    public bool hp;
    public bool mp;
    public bool speed;
    public bool atk;
    public bool spiritRange;

    [Header("Percentage Multipliers")]
    [Range(0.00f, 1.00f)]
    public float hpPercentage;
    public float mpPercentage;
    public float speedPercentage;
    public float atkPercentage;
    public float spiritRangePercentage;

    [Header("Set to specific ammount")]
    public int hpAmmount;
    public int mpAmmount;
    public int speedAmmount;
    public int atkAmmount;
    public int spiritRangeAmmount;

    public Image hudIcon;

    public GameObject UIButton;
}
