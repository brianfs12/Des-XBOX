using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Legs_Spit : MonoBehaviour
{
    public Boss_2_Legs_StateMachine stateMachine;
    public GameObject blood;
    public float waitToAttack;
    float counter;
    public GameObject laser;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        counter = waitToAttack;
    }

    private void OnEnable()
    {
        counter = waitToAttack;
        anim.SetTrigger("Attack");
        //Instantiate(blood, new Vector3(transform.position.x, 3.0f, transform.position.z), Quaternion.identity);
    }

    void Update()
    {
        counter -= Time.deltaTime;

        if(counter <= 0.0f)
        {
            //stateMachine.ActivateState(stateMachine.attack);
        }
    }

    public void DeactivateLaser()
    {
        laser.SetActive(false);
    }

    public void ActivateAttack()
    {
        stateMachine.ActivateState(stateMachine.attack);
    }

    public void ActivateLaser()
    {
        laser.SetActive(true);
    }
}
