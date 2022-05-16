using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu]
public class PossessionEssence : SpiritEssence
{
    public override void Activate(GameObject parent)
    {
        PlayerSpiritMode spirit = parent.GetComponent<PlayerSpiritMode>();
        if (spirit.T >= spirit.spiritCoolDown) {
            spirit.T = 0;
            spirit.gameObject.transform.GetChild(3).GetComponent<CircleCollider2D>().enabled = true;
            spirit.enabled = true;
            spirit.selectedEnemyIndex = 0;
            spirit.spirited = true;
            if (spirit.range.enemigosEnRango.Count > 0)
            {
                spirit.range.OrdenarEnemigos();
                spirit.selectedEnemyIndex = 0;
                spirit.AsignarEnemigoSeleccionado();
            }
            parent.GetComponent<PlayerLightController>().ToggleHeadLight();
            parent.GetComponent<PlayerLightController>().ToggleSpiritModeLight();
            spirit.SetSpirited();
        }
    }

    public override void Deactivate(GameObject parent)
    {
        PlayerSpiritMode spirit = parent.GetComponent<PlayerSpiritMode>();
        if (spirit.T >= spirit.spiritCoolDown)
        {
            spirit.T = 0;
            //spirit.gameObject.transform.GetChild(3).GetComponent<CircleCollider2D>().enabled = false;
            spirit.selectedEnemyIndex = 0;
            spirit.spirited = false;
            if (spirit.range.enemigosEnRango.Count > 0)
            {
                spirit.range.OrdenarEnemigos();
                spirit.AsignarEnemigoSeleccionado();
            }
            parent.GetComponent<Animator>().SetTrigger("SpiritOut");
            //spirit.enabled = false;
        }
    }
}
