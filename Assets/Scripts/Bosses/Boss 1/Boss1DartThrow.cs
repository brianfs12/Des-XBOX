using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1DartThrow : MonoBehaviour
{
    private GameObject player;
    private Vector3 relativePos;

    public GameObject proyectile;
    public float shotInterval;
    public float intervalVariation = 1;
    public float offset_rotación = 0.02f; //entre más grande, más tarda en rotar hacia el jugador
    public float proyectileSpeed;
    public Boss1Move boss;
    Boss1Sounds sounds;

    //private Quaternion proyectileRotation;

    public float timestamp = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponent<Boss1Sounds>();
        player = GameObject.FindGameObjectWithTag("Player");
        timestamp = shotInterval;
        boss = GetComponent<Boss1Move>();
        //proyectileRotation = proyectile.transform.rotation;
    }

    private void FixedUpdate()
    {
        if (Time.time > timestamp) //checa si ya es tiempo de disparar y si el jugador esta ahi
        {
            reiniciar_disparo(); //actualiza el tiempo para saber cuando va a disparar otra vez
            //los transform.localScale es para que si en el editor se invierte la escala ahora dispare al otro lado
            sounds.PlaySound("event:/enemies/bosses/boss1/throwAttack");
            GameObject spawnedObject = Instantiate(proyectile, transform.position + new Vector3(0.5f * transform.localScale.x, 0), transform.rotation);
            spawnedObject.transform.LookAt(player.transform);

            spawnedObject.GetComponent<Rigidbody2D>().velocity = transform.forward * proyectileSpeed;
            EnemyProyectileBehaviour script = spawnedObject.GetComponent<EnemyProyectileBehaviour>();
            script.sprite.color = boss.sprite_boss.color;
            var col = script.particles.colorOverLifetime;
            col.enabled = true;
            col.color = boss.particles.colorOverLifetime.color;
            script.luz.color = boss.luz.color;
            script.luz.intensity = boss.luz.intensity;

            if (this.boss.boss.currentHealth < this.boss.boss.maxHP / 2) {
                spawnedObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = boss.colorFase2Aura;
            }
            if (boss.hidden) {
                boss.hide();
            }
        }
    }

    public void reiniciar_disparo() {
        timestamp = Time.time + Random.Range(shotInterval - intervalVariation, shotInterval + intervalVariation);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(player.transform,transform.up);
        relativePos = player.transform.position - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(relativePos);
        LeanTween.rotate(this.gameObject, lookRot.eulerAngles, offset_rotación);
        //transform.rotation = new Quaternion(lookRot.x, lookRot.y,lookRot.z,lookRot.w);
    }
}
