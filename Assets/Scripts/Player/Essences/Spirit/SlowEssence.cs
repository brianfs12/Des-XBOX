using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

[CreateAssetMenu]
public class SlowEssence : SpiritEssence
{
    public override void Activate(GameObject parent)
    {
        Timekeeper timekeeper = FindObjectOfType<Timekeeper>();
        GlobalClock slowClock = timekeeper.Clock("AffectedByTimeSlow");
        slowClock.localTimeScale = 0.3f;
    }

    public override void Deactivate(GameObject parent)
    {
        Timekeeper timekeeper = FindObjectOfType<Timekeeper>();
        GlobalClock slowClock = timekeeper.Clock("AffectedByTimeSlow");
        slowClock.localTimeScale = 1f;
    }
}
