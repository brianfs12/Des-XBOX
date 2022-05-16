using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnstileScript : MonoBehaviour
{
    public enum Direction {Left, Right};
    public Direction direction;

    public Sprite leftSprite;
    public Sprite rightSprite;

    public Animator anim;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if(direction == Direction.Left)
        {
            spriteRenderer.sprite = leftSprite;
        }
        else if(direction == Direction.Right)
        {
            spriteRenderer.sprite = rightSprite;
        }
    }

    private void OnValidate()
    {
        if (direction == Direction.Left)
        {
            spriteRenderer.sprite = leftSprite;
        }
        else if (direction == Direction.Right)
        {
            spriteRenderer.sprite = rightSprite;
        }
    }

    private void Update()
    {
        //Mierda de debug
        /*
        if(Input.GetKeyDown(KeyCode.Q))
        {
            anim.Play("TurnLeft");
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            anim.Play("TurnRight");
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.Distance(this.gameObject.GetComponent<BoxCollider2D>()).normal.x < 0)
            {
                anim.Play("TurnLeft");
            }
            else if(collision.Distance(this.gameObject.GetComponent<BoxCollider2D>()).normal.x > 0)
            {
                anim.Play("TurnRight");
            }
        }
    }

    private void OnDisable()
    {
        transform.rotation = Quaternion.identity;
    }
}
