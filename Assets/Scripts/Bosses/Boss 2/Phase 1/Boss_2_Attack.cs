using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Attack : MonoBehaviour
{
    public Boss_2_StateMachine stateMachine;
    Vector3 playerPos;
    public float attackSpeed;

    public float timeToAttack;
    float counter;
    bool playSoundOnce;
    FMOD.Studio.EventInstance lungeSound;

    Animator anim;

    private void OnEnable()
    {
        playSoundOnce = false;
        counter = timeToAttack;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        counter = timeToAttack;
    }

    void Update()
    {
        counter -= Time.deltaTime;

        if (counter <= 0.0f)
        {
            LungePlayer();
        }

        if (Vector3.Distance(transform.position, playerPos) < 0.1f)
        {
            anim.SetBool("Flying", true);
            stateMachine.ActivateState(stateMachine.back);
        }
    }

    void LungePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerPos, attackSpeed * Time.deltaTime);
        if (!playSoundOnce) {
            playSoundOnce = true;
            PlayLungeSound();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && this.isActiveAndEnabled)
        {
            stateMachine.ActivateState(stateMachine.back);
        }
    }

    public void PlayLungeSound()
    {
        lungeSound = FMODUnity.RuntimeManager.CreateInstance("event:/enemies/bosses/boss2/lunge");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(lungeSound, transform);
        lungeSound.start();
        lungeSound.release();
    }
}
