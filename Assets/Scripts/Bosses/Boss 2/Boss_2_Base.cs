using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Base : MonoBehaviour
{
    [Header("Stats")]
    public int currentHealth;
    public int damageToPlayer;
    public int maxHP;

    [Header("First Phase States")]
    public MonoBehaviour[] firstStates = new MonoBehaviour[4];
    public GameObject transitionAnimation;

    [Header("Chest States")]
    public MonoBehaviour[] chestStates = new MonoBehaviour[3];

    [Header("Legs States")]
    public MonoBehaviour[] legsStates = new MonoBehaviour[3];

    Animator anim;
    public Boss_2_Scythe scythe;
    public Boss_2_Base legs;
    public Boss_2_Base chest;
    public Animator bossUIAnim;
    public MetalCurtains metalCurtains;
    public Animator animCircles;
    public Boss_2_IntroTrigger boss2TriggerScript;

    private void Awake()
    {
        maxHP = currentHealth;
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        //Destruirse si ya fue derrotado
        if (DataManager.instance.boss2)
        {
            Destroy(gameObject);
        }
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TransitionMovement()
    {
        for (int i = 0; i < firstStates.Length; i++)
        {
            firstStates[i].enabled = false;
        }
        
        LeanTween.moveLocal(gameObject, new Vector3(7.0f, 0.3f, 0.0f), 1.0f).setOnComplete(TransitionAnimation);
    }

    public void TransitionAnimation()
    {
        animCircles.SetTrigger("Die");
        gameObject.SetActive(false);
        Destroy(gameObject);
        transitionAnimation.SetActive(true);
    }

    public void Die()
    {
        //Animacion de muerte
        if (gameObject.name == "Boss2_FirstPhase")
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            GameManager.Instance.StopPlayer();
            GameManager.Instance.canPause = false;
            TransitionMovement();
            scythe.Die();
        }
        else if(gameObject.name == "Boss2_Chest")
        {
            for (int i = 0; i < chestStates.Length; i++)
            {
                chestStates[i].enabled = false;
            }
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<Boss_2_Chest_Move>().death = true;
            LeanTween.moveLocalY(gameObject, 0.0f, 0.6f).setOnComplete(ChestDie);
        }
        else if (gameObject.name == "Boss2_Legs")
        {
            for (int i = 0; i < legsStates.Length; i++)
            {
                legsStates[i].enabled = false;
            }
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            if (legs.currentHealth <= 0 && chest.currentHealth <= 0)
            {
                bossUIAnim.SetBool("die", true);
                bossUIAnim.SetBool("intro", true);
                boss2TriggerScript.cam2.SetActive(false);
                metalCurtains.openCurtain();
                boss2TriggerScript.cinemachineBrain.m_DefaultBlend.m_Style = Cinemachine.CinemachineBlendDefinition.Style.Cut;
                //Activar el bool que guarda si ya fue derrotado
                GameManager.Instance.boss2 = true;
                GameManager.Instance.red = true; //Desbloquear teleport rojo
            }
            anim.SetTrigger("Death");
        }
    }

    public void PlayTorsoDeathSound(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }

    void ChestDie()
    {
        if (legs.currentHealth <= 0 && chest.currentHealth <= 0)
        {
            bossUIAnim.SetBool("die", true);
            boss2TriggerScript.cam2.SetActive(false);
            metalCurtains.openCurtain();
            boss2TriggerScript.cinemachineBrain.m_DefaultBlend.m_Style = Cinemachine.CinemachineBlendDefinition.Style.Cut;
            //Activar el bool que guarda si ya fue derrotado
            GameManager.Instance.boss2 = true;
            GameManager.Instance.red = true; //Desbloquear teleport rojo
        }
        anim.SetTrigger("Death");
    }

    public void NextPhase()
    {
        if (gameObject.name == "Boss2_FirstPhase")
        {
            gameObject.SetActive(false);
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth pH = collision.gameObject.GetComponent<PlayerHealth>();
            if (pH != null)
            {
                if (damageToPlayer != -1)
                {
                    pH.TakeDamage(damageToPlayer, this.gameObject);//hacer daño al jugador cuando chocan
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth pH = collision.gameObject.GetComponent<PlayerHealth>();
            if (pH != null)
            {
                if (damageToPlayer != -1)
                {
                    pH.TakeDamage(damageToPlayer, this.gameObject);//hacer daño al jugador cuando chocan
                }
            }
        }
    }
    public void PlayLegsDeathSound(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }
    public void PlayLegsWalkSound(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }
}
