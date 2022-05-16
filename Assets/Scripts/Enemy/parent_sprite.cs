using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parent_sprite : MonoBehaviour
{
    private SpriteRenderer parentSprite;
    private SpriteRenderer mySprite;

    // Start is called before the first frame update
    void Start()
    {
        parentSprite = gameObject.transform.parent.gameObject.GetComponent<SpriteRenderer>();
        mySprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mySprite.sprite != parentSprite.sprite) {
            mySprite.sprite = parentSprite.sprite;
        }
    }
}
