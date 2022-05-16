using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatEssenceHolder : MonoBehaviour
{
    public CombatEssence combatEssence;

    //Tenemos que referenciar el MP del jugador aqui
    public int playerMP;

    public void useEssence(InputAction.CallbackContext context)
    {
        if (context.performed && !GameManager.Instance.stopPlayer)
        {
            if(combatEssence != null)
            {
                if (GameManager.Instance.playerStats.currentMP > combatEssence.mpCostPerUse)
                {
                    GameManager.Instance.playerStats.currentMP -= combatEssence.mpCostPerUse;
                    combatEssence.Activate(this.gameObject);
                }
            }
        }
    }
}
