using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GuardEssence : SpiritEssence
{
    public override void Activate(GameObject parent)
    {
        Debug.Log("Guard active!");
        GameObject tmp = parent.GetComponent<PlayerObjectReferences>().guardEssenceGO;
        tmp.SetActive(true);
    }

    public override void Deactivate(GameObject parent)
    {
        Debug.Log("Guard deactivate!");
        GameObject tmp = parent.GetComponent<PlayerObjectReferences>().guardEssenceGO;
        tmp.SetActive(false);
    }
}
