using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    public Sprite[] sprites;
    static int last_index=-1;
    public SpriteRenderer auraSprite;
    // Start is called before the first frame update
    private void Awake()
    {
        int index = Random.Range(0, sprites.Length - 1);
        while (index == last_index) {
            index = Random.Range(0, sprites.Length - 1);
        }
        last_index = index;
        GetComponent<SpriteRenderer>().sprite = sprites[index];
        auraSprite.sprite = sprites[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
