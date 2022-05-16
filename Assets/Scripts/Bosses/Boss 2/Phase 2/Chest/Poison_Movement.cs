using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison_Movement : MonoBehaviour
{
    Vector3 playerPos;
    Vector3 targetDirection;
    public GameObject poisonPool;
    FMOD.Studio.EventInstance poisonImpactSound;


    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        targetDirection = (playerPos - transform.position).normalized;
    }

    void Update()
    {
        transform.position += targetDirection * 15f * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("floor") || collision.gameObject.CompareTag("Finish"))
        {
            Instantiate(poisonPool, collision.contacts[0].point, Quaternion.identity);
            PlayPoisonSound();
            Destroy(gameObject);
        }
    }
    public void PlayPoisonSound()
    {
        poisonImpactSound = FMODUnity.RuntimeManager.CreateInstance("event:/enemies/bosses/boss2/spitImpact");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(poisonImpactSound, transform);
        poisonImpactSound.start();
        poisonImpactSound.release();
    }

}
