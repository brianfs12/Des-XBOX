using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitobarashiSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject hitobarashi;
    private void OnEnable()
    {
        hitobarashi.SetActive(true);
    }
}
