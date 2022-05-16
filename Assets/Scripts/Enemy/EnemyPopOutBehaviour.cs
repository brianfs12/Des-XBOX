using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPopOutBehaviour : EnemyBase
{
    Animator spriteAnimator;
    public bool isOn;
    public float distance;
    public Collider2D collider;

    private void OnEnable()
    {
        Respawn();
        isOn = false;
    }

    private void Start()
    {
        spriteAnimator = gameObject.GetComponent<Animator>();
        isOn = false;
    }

    public void Update()
    {
        if (Vector2.Distance(transform.position, playerGO.transform.position) <= distance && !isOn)
        {
            KyuuRise();
        }
        else if (Vector2.Distance(transform.position, playerGO.transform.position) > distance && isOn)
        {
            KyuuHide();
        }
    }

    public void KyuuRise()
    {
        isOn = true;
        //gameObject.SetActive(true);
        collider.enabled = true;
        spriteAnimator.SetTrigger("Enter");
        FMODUnity.RuntimeManager.PlayOneShot("event:/enemies/mobs/kyuuRise", transform.position);
    }

    public void KyuuHide()
    {
        isOn = false;
        collider.enabled = false;
        spriteAnimator.SetTrigger("hide");
        FMODUnity.RuntimeManager.PlayOneShot("event:/enemies/mobs/kyuuHide", transform.position);
    }
    
}
