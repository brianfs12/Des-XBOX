using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D;

public class PossessionPoints : MonoBehaviour
{
    public enum AuraColor{Blue, Green, Red, Food};
    public AuraColor auraColor;

    public Sprite blueSprite;
    public Sprite greenSprite;
    public Sprite redSprite;
    Color blue = new Color(101, 181, 221);
    Color green = new Color(101, 221, 101);
    Color red = new Color(221, 101, 101);
    public UnityEngine.Experimental.Rendering.Universal.Light2D auraLight;

    public SpriteRenderer spriteRenderer;
    public bool poseido;

    //Hace que los cambios se vean reflejados inmediatamente desde que haces el cambio en el inspector.
    private void OnValidate()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        if (auraColor == AuraColor.Blue)
        {
            spriteRenderer.sprite = blueSprite;
            auraLight.color = blue;
        }
        if (auraColor == AuraColor.Red)
        {
            spriteRenderer.sprite = redSprite;
            auraLight.color = red;
        }
        if (auraColor == AuraColor.Green)
        {
            spriteRenderer.sprite = greenSprite;
            auraLight.color = green;
        }
        if(auraColor != AuraColor.Food)
            auraLight.intensity = 0.01f;
    }

    // Start is called before the first frame update
    void Start()
    {
        poseido = false;
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        
        if(auraColor == AuraColor.Blue)
        {
            spriteRenderer.sprite = blueSprite;
            auraLight.color = blue;
        }
        if (auraColor == AuraColor.Red)
        {
            spriteRenderer.sprite = redSprite;
            auraLight.color = red;
        }
        if (auraColor == AuraColor.Green)
        {
            spriteRenderer.sprite = greenSprite;
            auraLight.color = green;
        }
        if (auraColor != AuraColor.Food)
            auraLight.intensity = 0.01f;
    }


}
