using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpiritAbilityHolder : MonoBehaviour
{
    public SpiritEssence spiritEssence;
    public bool canToggle = true;
    public bool essenceActive = false;
    public Animator anim;

    public PlayerSpiritEnergy spiritEnergy;

    private void Awake()
    {
        spiritEnergy = GetComponent<PlayerSpiritEnergy>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.Instance.playerStats.currentMP <= 0 && essenceActive)
        {
            spiritEnergy.refill = true;
            essenceActive = false;
            spiritEssence.Deactivate(this.gameObject);
        }
    }

    public void toggleEssence(InputAction.CallbackContext context)
    {
        if (context.performed && !GameManager.Instance.stopPlayer)
        {
            if (spiritEssence != null && canToggle)
            {
                if (!essenceActive)
                {
                    spiritEnergy.refill = false;
                    essenceActive = true;
                    if (anim.GetBool("canSpirit"))
                    {
                        spiritEssence.Activate(this.gameObject);
                    }
                }
                else
                {
                    spiritEnergy.refill = true;
                    essenceActive = false;
                    if (anim.GetBool("canSpirit"))
                    {
                        spiritEssence.Deactivate(this.gameObject);
                    }
                }
            }
        }
    }

    public void turnOffEssence(InputAction.CallbackContext context)
    {
        if (context.performed && !GameManager.Instance.stopPlayer)
        {
            if (spiritEssence != null)
            {
                if (essenceActive)
                {
                    essenceActive = false;
                    spiritEssence.Deactivate(this.gameObject);
                }
            }
        }
    }

    public void ForceEssenceOff()
    {
        if (spiritEssence != null)
        {
            if (essenceActive)
            {
                essenceActive = false;
                spiritEssence.Deactivate(this.gameObject);
            }
        }
    }
}
