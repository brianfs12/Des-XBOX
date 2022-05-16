using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Chest_Attack : MonoBehaviour
{
    public Boss_2_Chest_StateMachine stateMachine;
    Vector3 playerPos;
    public float attackSpeed;

    public GameObject poisonDrop;

    public float timeToAttack;
    float counter;

    Animator anim;
    bool canSpit;
    FMOD.Studio.EventInstance spit;


    private void OnEnable()
    {
        counter = timeToAttack;
        playerPos = GameObject.Find("Playeer").transform.position;
        canSpit = true;
    }

    void Start()
    {
        counter = timeToAttack;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        counter -= Time.deltaTime;

        if (counter <= 0.0f)
        {
            counter = timeToAttack;
            if (canSpit)
            {
                SpitPoison();
            }
        }
    }

    void SpitPoison()
    {
        canSpit = false;
        anim.SetTrigger("Spit");
        PlaySpitSound();
    }
    public void PlaySpitSound()
    {
        spit = FMODUnity.RuntimeManager.CreateInstance("event:/enemies/bosses/boss2/spitAttack");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(spit, transform);
        spit.start();
        spit.release();
    }

    public void InstantiateBloodBall()
    {
        Instantiate(poisonDrop, transform.position, Quaternion.identity);
        StartCoroutine(WaitAfterAttack());
    }

    IEnumerator WaitAfterAttack()
    {
        yield return new WaitForSeconds(0.8f);
        stateMachine.ActivateState(stateMachine.move);
    }
}
