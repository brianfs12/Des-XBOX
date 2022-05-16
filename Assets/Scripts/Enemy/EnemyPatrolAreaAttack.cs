using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolAreaAttack : EnemyBase
{
    public int maxHealth;
    public float speed;

    [Header("Misc")]
    public float waitTime;
    public float startWaitTime;
    public GameObject attackGO;
    public bool imOnPlace;
    public Animator animator;
    public Animator animatorSpickes;
    public bool attacked;
    Quaternion currentRotation = new Quaternion(0,0,0,0);
    FMOD.Studio.EventInstance movement;

    [Header("Areas")]
    [SerializeField]
    Transform[] points;
    public int actualSpot;


    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        speed = 5;
        actualSpot = 0;
        imOnPlace = false;
        maxHealth = 3;
        currentHealth = maxHealth;
        attacked = false;
    }

    private void OnEnable()
    {
        waitTime = startWaitTime;
        speed = 5;
        actualSpot = 0;
        imOnPlace = false;
        maxHealth = 3;
        attacked = false;
        transform.rotation = currentRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, points[actualSpot].position) < 0.1f)
        {
            imOnPlace = true;

        }
        
        if(imOnPlace)
        {
            if (!attacked)
            {
                attacked = true;
                animator.SetTrigger("Attack");
                movement.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

            }
            waitTime -= Time.deltaTime;
        }
        else
        {
            UnshowAttack();
        }
        if (!imOnPlace)
        {
            transform.position = Vector2.MoveTowards(transform.position, points[actualSpot].position, speed * Time.deltaTime);
        }
    }
    void ShowAttack()
    {
        attackGO.SetActive(true);
    }

    void UnshowAttack()
    {
        attackGO.SetActive(false);
    }

    public void MakeDamage(int damage)
    {
        if (pH != null)
        {
            if (damage != -1)
            {
                pH.TakeDamage(damage, this.gameObject);
            }
        }
    }

    public void PlaySlapSound(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }

    public void PlayMovementEvent()
    {
        if (!imOnPlace)
        {
            movement = FMODUnity.RuntimeManager.CreateInstance("event:/enemies/mobs/hitobashiraMove");
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(movement, transform);
            movement.start();
            movement.release();
        }
    }
}