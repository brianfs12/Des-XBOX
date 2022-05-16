using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class EnemyDartThrower : EnemyBase
{
    public GameObject proyectileBlue;
    public GameObject proyectileRed;
    public float shotInterval;
    public float proyectileSpeed;
    public int shotColorIndex;

    float timestamp = 0.0f;
    Animator spriteAnimator;
    Timeline timeline;

    bool playerDetected;

    private void Start()
    {
        spriteAnimator = GetComponent<Animator>();
        timeline = GetComponent<Timeline>();
    }

    private void OnEnable()
    {
        timestamp = 0.0f;
    }

    private void FixedUpdate()
    {
        if(shotInterval > 0)
        {
            shotInterval -= Time.fixedDeltaTime * timeline.timeScale;
        }
        else
        {
            shotColorIndex = Random.Range(1, 3); //seleccionar entre disparo rojo o azul
            spriteAnimator.SetTrigger("Throw " + shotColorIndex); //llama la funcion ThrowTofu con un animation event
            shotInterval = 3f;
        }

        /*
        if (Time.deltaTime > timestamp) //checa si ya es tiempo de disparar y si el jugador esta ahi
        {
            //Debug.Log("Dispara");
            timestamp = Time.deltaTime + shotInterval; //actualiza el tiempo para saber cuando va a disparar otra vez


            shotColorIndex = Random.Range(1, 3); //seleccionar entre disparo rojo o azul
            spriteAnimator.SetTrigger("Throw " + shotColorIndex); //llama la funcion ThrowTofu con un animation event            
        }
        */
    }

    void ThrowPaper() //Se llama con un animation event de Throw
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/enemies/mobs/akamantoThrow", transform.position);
        //los transform.localScale es para que si en el editor se invierte la escala ahora dispare al otro lado
        GameObject spawnedObject;
        if (shotColorIndex == 2)
            spawnedObject = Instantiate(proyectileBlue, transform.position + new Vector3(0.5f * transform.localScale.x, 0.2f), Quaternion.identity);
        else
            spawnedObject = Instantiate(proyectileRed, transform.position + new Vector3(0.5f * transform.localScale.x, 0.2f), Quaternion.identity);

        spawnedObject.transform.parent = gameObject.transform;
        spawnedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(proyectileSpeed * transform.localScale.x, 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //iniciar disparo
            playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //detener disparo
            playerDetected = false;

        }
    }
}
