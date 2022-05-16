using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class player_controler : MonoBehaviour
{
    private Rigidbody2D rigi;
    public Animator anim;
    [SerializeField] Collider2D hit_box_normal;
    [SerializeField] Collider2D hit_box_agachado;

    private SpriteRenderer sprite;
    public float walk_speed = 2;
    public float jump_force = 0;
    public bool grounded = false;
    public bool spirited = false;
    public bool agachandose = false;

    public BetterJumping betterJump;

    /*void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    /*
    void LateUpdate()
    {
        move();
        input_jump();
        spirit_input();
        fall();
        click();
        agachado();
    }
    

    void fall() {
        if (rigi.velocity.y < -1.0f && !grounded)
        {
            grounded = false;
            anim.Play("jump");
        }
    }

    void spirit_input() {
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && !agachandose)
        {
            if(!spirited)
            {
                print("Presionaste S");
                anim.SetBool("spirited_out", false);
                spirited = true;
                rigi.velocity = Vector2.zero;
                rigi.constraints = RigidbodyConstraints2D.FreezeAll;
                anim.SetTrigger("spirited");
            }
            else
            {
                spirited = false;
                rigi.constraints = RigidbodyConstraints2D.FreezeRotation;
                anim.SetBool("spirited_out", true);
                rigi.velocity = Vector2.down;
            }
        }
    }

    void input_jump() {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && grounded && !spirited && !agachandose)
        {
            //grounded = false;
            anim.SetTrigger("init_jump");
        }
    }

    public void jump() {
        rigi.AddForce(Vector2.up * jump_force * 10, ForceMode2D.Impulse);
    }

    void move() {
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && !spirited && !agachandose)
        {
            rigi.velocity = new Vector2(walk_speed, rigi.velocity.y);
            anim.SetBool("running", true);
            if (sprite.flipX)
            {
                sprite.flipX = false;
            }
        }
        else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !spirited && !agachandose)
        {
            rigi.velocity = new Vector2(-walk_speed, rigi.velocity.y);
            anim.SetBool("running", true);
            if (!sprite.flipX)
            {
                sprite.flipX = true;
            }
        }
        else
        {
            rigi.velocity = new Vector2(0f, rigi.velocity.y);
            anim.SetBool("running", false);
        }
    }

    */public void teletransportar(Transform enemy_transfor) {
        if (spirited) {
            transform.position = new Vector3(enemy_transfor.position.x,enemy_transfor.position.y,transform.position.z);
        }
    }/*

    void click()
    {
        if (Input.GetMouseButtonDown(0) && spirited)
        {
            Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitinfo = Physics2D.Raycast(rayOrigin, Vector2.zero);

            if (hitinfo.collider != null)
            {

                if (hitinfo.transform.CompareTag("enemy")) {
                    teletransportar(hitinfo.transform);
                    Destroy(hitinfo.transform.gameObject);
                }

            }
        }
        if (Input.GetMouseButtonDown(1) && spirited)
        {
            Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitinfo = Physics2D.Raycast(rayOrigin, Vector2.zero);

            if (hitinfo.collider != null)
            {

                if (hitinfo.transform.CompareTag("enemy"))
                {
                    teletransportar(hitinfo.transform);
                }

            }
        }
    }

    void agachado() {
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) && grounded))
        {
            if (!agachandose) {
                anim.SetBool("agacharse", true);
                agachandose = true;
                hit_box_agachado.enabled = true;
                hit_box_normal.enabled = false;
            }
        }
        else {
            if (agachandose) {
                anim.SetBool("agacharse", false);
                hit_box_normal.enabled = true;
                hit_box_agachado.enabled = false;
                agachandose = false;
            }
        }
    }*/
}
