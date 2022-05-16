using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Boss1Move : MonoBehaviour
{
    public bool hidden;
    public float movementSpeed;

    [Header("Target")]
    public GameObject[] positionTargets;
    private int indexTarget = 0;
    private Vector3 currentTarget;
    public GameObject target_object;
    public Boss1Base boss;
    public List<GameObject> enemigos;
    private GameObject player;
    private Boss1DartThrow disparador;
    private Boss1Sounds sounds;

    int index;
    int previousInddex;
    PossessionPoints enemigoScript;

    [Header("Animacion")]
    public Animator anim;
    public bool intro = true;

    [Header("Fase 2")]
    public SpriteRenderer sprite_aura;
    public SpriteRenderer sprite_boss;
    public UnityEngine.UI.Image health_bar;
    bool cambiarFase = true;
    public Color colorFase2Aura;
    public Color colorFase2Sprite;
    public Gradient colorFase2particles;
    public int noMovidas = 3;
    public int movidasAntesDeCargar = 2;

    public float multVelocidad = 2;

    public List<GameObject> enemigos_totales;

    [System.NonSerialized]public ParticleSystem particles;
    public Light2D luz;



    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponent<Boss1Sounds>();
        boss = GetComponent<Boss1Base>();
        anim = GetComponent<Animator>();
        disparador = GetComponent<Boss1DartThrow>();
        player = GameObject.FindGameObjectWithTag("Player");
        //enemigos_totales = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        previousInddex = -1;
        hidden = false;

        particles = gameObject.GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (intro || boss.currentHealth<=0) {
            disparador.reiniciar_disparo(); 
            return;
        } 

        if (!hidden) {
            move();
            if (boss.currentHealth < boss.maxHP)
            {
                if (boss.currentHealth % 50 == 0 && enemigos.Count>0)
                {
                    anim.SetTrigger("hide");
                    hidden = true;
                }
            }
        }

        if (cambiarFase == true && boss.currentHealth <= boss.maxHP / 2) {
            cambiarFase = false;
            //cambiar color
            if (sprite_aura.color != colorFase2Aura) {
                sounds.PlaySound("event:/enemies/bosses/boss1/beginPhase2");
                print("cambiar_color");
                sprite_aura.color = colorFase2Aura;
                sprite_boss.color = colorFase2Sprite;
                luz.color = colorFase2Sprite;
                luz.intensity = 1.5f;
                var col = particles.colorOverLifetime;
                col.enabled = true;
                col.color = colorFase2particles;
                //health_bar.color = colorFase2Sprite;
                for (int i = 0; i < enemigos_totales.Count; i++)
                {
                    GameObject child = enemigos_totales[i].transform.GetChild(0).gameObject;
                    child.SetActive(true);
                    child.GetComponent<SpriteRenderer>().color = colorFase2Aura;
                    child.GetComponentInChildren<Light2D>().color = colorFase2Aura;
                    child.SetActive(false);
                }
            }
        }

        if (enemigos.Count > 0 && enemigoScript)
        {
            if (enemigoScript.poseido)
            {
                print("salte puto");
                unhide();
            }
        }
    }

    void desIntro() {
        intro = false;
    }

    void move() {
        target_object = positionTargets[indexTarget];
        float stepSpeed = movementSpeed * Time.deltaTime;//velocidad indepenediente del frame rate

        //se está moviendo hacia la pocición del jugador?
        if (currentTarget != player.transform.position)
        {
            currentTarget = positionTargets[indexTarget].transform.position;
        }
        else {
            currentTarget = player.transform.position;
            stepSpeed *= multVelocidad;
        }

        transform.position = Vector2.MoveTowards(transform.position, currentTarget, stepSpeed);//moverse hacia la pocición del target

        if (transform.position == currentTarget)
        {
            if (currentTarget != player.transform.position) //se está moviendo hacia la pocición del jugador?
            {
                indexTarget++;
            }
            if (indexTarget == positionTargets.Length)//reiniciar los targets cuando se llegue al ultimo
            {
                indexTarget = 0;
            }
            if (boss.currentHealth <= boss.maxHP / 2) //esta en segunda fase?
            {
                noMovidas++;
                if (noMovidas % (movidasAntesDeCargar + 1) == 0) //se movio hacia x numero de targets que es multiplo de n+1?
                {
                    sounds.PlaySound("event:/enemies/bosses/boss1/lungeAttack");
                    currentTarget = player.transform.position; //dive hacia el jugador
                }
                else {
                    currentTarget = positionTargets[indexTarget].transform.position; //seguir moviendose hacia los targets
                }
            }
        }
    }

    public void hide() {
        sounds.PlaySound("event:/enemies/bosses/boss1/hide");
        luz.enabled = false;
        if (particles.gameObject.activeSelf) {
            particles.gameObject.SetActive(false);
        }

        //escojer un enemigo aleatorio y se teletransporta a su pocición
        index = Random.Range(0, enemigos.Count);
        if (enemigos.Count > 1)
        {
            while (index == previousInddex)//que no sea el que sea el mismo que se acaba de escojer
            {
                index = Random.Range(0, enemigos.Count);
            }
        }
        else {
            hide();
        }

        desactivar_auras();
        transform.position = enemigos[index].transform.position;
        if (boss.currentHealth > boss.maxHP / 2)
        {
            enemigos[index].transform.GetChild(0).gameObject.SetActive(true);
        }
        else {
            Invoke("activar_auras", 0.001f);
        }
        enemigoScript = enemigos[index].GetComponent<PossessionPoints>();
        previousInddex = index;
    }

    void desactivar_auras() {
        for (int i = 0; i < enemigos_totales.Count; i++)
        {
            enemigos_totales[i].transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void activar_auras() {
        print("activar auras");
        for (int i = 0; i < enemigos.Count; i++)
        {
            enemigos[i].transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void unhide() {
        if (hidden) {
            sounds.PlaySound("event:/enemies/bosses/boss1/expelled");
            luz.enabled = true;
            particles.gameObject.SetActive(true);
            disparador.reiniciar_disparo();
            hidden = false;
            desactivar_auras();
            anim.SetTrigger("out");
            boss.TakeDamage(10);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss1PossessionPoint")) {
            enemigos.Add(collision.gameObject);
        }
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth pH = collision.gameObject.GetComponent<PlayerHealth>();
            if (pH != null)
            {
                if (boss.damageToPlayer != -1)
                {
                    pH.TakeDamage(boss.damageToPlayer, this.gameObject);//hacer daño al jugador cuando chocan
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Boss1PossessionPoint"))
        {
            enemigos.Remove(other.gameObject);
        }
    }

}
