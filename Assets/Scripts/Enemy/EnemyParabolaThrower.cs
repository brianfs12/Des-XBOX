using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class EnemyParabolaThrower : EnemyBase
{
    public GameObject proyectile;
    public float shotInterval;
    public float proyectileSpeed_x;
    public float proyectileSpeed_y;

    float timestamp = 0.0f;
    Animator spriteAnimator;
    Timeline timeline;

    private void Start()
    {
        timestamp = shotInterval;
        spriteAnimator = GetComponent<Animator>();
        timeline = GetComponent<Timeline>();
    }

    private void OnEnable()
    {
        timestamp = shotInterval;
    }

    private void FixedUpdate()
    {
        if (timestamp > 0) //checa si ya es tiempo de disparar
        {
            timestamp -= Time.fixedDeltaTime * timeline.timeScale; //actualiza el tiempo para saber cuando va a disparar otra vez
        }
        else
        {
            spriteAnimator.SetTrigger("Throw");
            timestamp = shotInterval;
        }
    }

    void ThrowTofu() //Se llama con un animation event de Throw
    {
        //los transform.localScale es para que si en el editor se invierte la escala ahora dispare al otro lado
        GameObject spawnedObject = Instantiate(proyectile, transform.position + new Vector3(0.5f * transform.localScale.x, 0), Quaternion.identity);
        spawnedObject.transform.parent = gameObject.transform;
        spawnedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(proyectileSpeed_x * transform.localScale.x, proyectileSpeed_y * transform.localScale.y);
        FMODUnity.RuntimeManager.PlayOneShot("event:/enemies/mobs/tofuThrow", transform.position);
    }
}
