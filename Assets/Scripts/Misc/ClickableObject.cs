using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour, IPointerClickHandler
{
    public PlayerSpiritMode player;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (player.spirited) {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                player.teletransportar(GetComponent<RectTransform>());
                Destroy(gameObject);
            }

            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                player.teletransportar(GetComponent<RectTransform>());
            }
        }
    }

}
