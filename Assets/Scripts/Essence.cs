//Este script se coloca en todos los GO esencia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MoreMountains.Feedbacks;

public enum essenceType
{
    Passive, Spirit, Combat
};

public class Essence : MonoBehaviour
{
    public essenceType type;

    public CombatEssence combatObject;
    public SpiritEssence spiritObject;
    public PassiveEssence passiveObject;
    public bool enseñarTexto = false;

    [SerializeField] private MMFeedback flashReaccion;
    private ShowItemText TextRecojer;

    private void Awake()
    {
        flashReaccion = gameObject.GetComponentInParent<MMFeedback>();
        TextRecojer = gameObject.GetComponentInParent<ShowItemText>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (enseñarTexto) {
                TextRecojer.StartTextBox();
            }
            //print("play flash");
            flashReaccion?.Play(transform.position);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Misc/powerUpRecieved", transform.position);
            GameManager.Instance.gameProgress.AddEssence(this);
            Destroy(this.gameObject);
        }
    }
}

/*[CustomEditor(typeof(Essence))]
public class MyTestEditor : Editor
{
    Essence mTest;

    void OnEnable()
    {
        mTest = (Essence)target;
    }
    public override void OnInspectorGUI()
    {
        mTest.type = (essenceType)EditorGUILayout.EnumPopup("EssenceType", mTest.type);
        switch (mTest.type)
        {
            case essenceType.Combat:
                {
                    mTest.combatObject = (CombatEssence)EditorGUILayout.ObjectField("CombatObject", mTest.combatObject, typeof(CombatEssence), true);
                    break;
                }
            case essenceType.Passive:
                {
                    mTest.passiveObject = (PassiveEssence)EditorGUILayout.ObjectField("PassiveObject", mTest.passiveObject, typeof(PassiveEssence), true);
                    break;
                }
            case essenceType.Spirit:
                {
                    mTest.spiritObject = (SpiritEssence)EditorGUILayout.ObjectField("SpiritObject", mTest.spiritObject, typeof(SpiritEssence), true);
                    break;
                }
        }
    }
}*/