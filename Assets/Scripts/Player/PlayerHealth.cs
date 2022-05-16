using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using MoreMountains.Feedbacks;

public class PlayerHealth : MonoBehaviour
{
    public GameObject interactCollider;
    public GameObject transitionCheck;

    bool canTakeDamage;
    bool death = false;
    bool parpadear;
    SpriteRenderer rend;
    Color originalColor;
    Color transparencia;
    Animator animator;
    public MMFeedback dieReaccion;

    [SerializeField]
    public float invulnerabilitySeconds;
    float seconds;
    [Header("konckback")]
    Rigidbody2D playerRigi;
    public float knockForce = 4000f;

    void Start()
    {
        seconds = invulnerabilitySeconds;
        rend = GetComponent<SpriteRenderer>();
        originalColor = rend.material.color;
        animator = GetComponent<Animator>();
        playerRigi = GetComponent<Rigidbody2D>();
        if (dieReaccion == null) {
            dieReaccion = gameObject.GetComponentInChildren<MMFeedback>();
        }
    }

    void Update()
    {
        if(!canTakeDamage)
        {
            if (death) return;
            seconds -= Time.deltaTime;
            if(seconds <= 0.0f)
            {
                parpadear = false;
                rend.color = originalColor;
                canTakeDamage = true;
                seconds = invulnerabilitySeconds;
                //Regresar a su capa original los colliders del jugador
                gameObject.layer = 3;
                interactCollider.layer = 3;
                transitionCheck.layer = 3;
            }
        }
        if(parpadear)
        {
            transparencia = rend.color;
            transparencia.a = Mathf.PingPong(Time.time * 10, 1.0f);
            rend.color = transparencia;
        }
    }

    WaitForSeconds vibrationTime = new WaitForSeconds(0.5f);

    IEnumerator Vibration()
    {
        //Comenzar vibracion
        GamePad.SetVibration(PlayerIndex.One, 0.1f, 0.1f);
        yield return vibrationTime;
        //Detener vibracion
        GamePad.SetVibration(PlayerIndex.One, 0.0f, 0.0f);
    }

    public void TakeDamage(int _damage, GameObject damager)
    {
        if(canTakeDamage)
        {
            StartCoroutine(Vibration());
            parpadear = true;
            canTakeDamage = false;
            //Cambiar de layer todos los colliders del jugador
            gameObject.layer = 10;
            interactCollider.layer = 10;
            transitionCheck.layer = 10;

            GameManager.Instance.playerStats.currentHealth -= _damage;

            if(GameManager.Instance.playerStats.currentHealth <= 0)
            {
                Die();
            }

            Knockback(damager);

            FMODUnity.RuntimeManager.PlayOneShot("event:/playerSounds/recieveDamage", transform.position);

        }
    }

    public void TakeDamageBoss3(int _damage) {
        StartCoroutine(Vibration());
        parpadear = true;
        canTakeDamage = false;
        //Cambiar de layer todos los colliders del jugador
        gameObject.layer = 10;
        interactCollider.layer = 10;
        transitionCheck.layer = 10;

        GameManager.Instance.playerStats.currentHealth -= _damage;

        if (GameManager.Instance.playerStats.currentHealth <= 0)
        {
            Die();
        }

        playerRigi.AddForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized * knockForce + playerRigi.velocity, ForceMode2D.Force);

        FMODUnity.RuntimeManager.PlayOneShot("event:/playerSounds/recieveDamage", transform.position);
    }

    public void Knockback(GameObject obj) {
        Rigidbody2D _rigi = obj.GetComponent<Rigidbody2D>();
        if (_rigi)
        {
            knock(_rigi);
        }
        else {
            _rigi = obj.GetComponentInChildren<Rigidbody2D>();
            if (_rigi)
            {
                knock(_rigi);
            }
            else {
                _rigi = obj.GetComponentInParent<Rigidbody2D>();
                if (_rigi)
                {
                    knock(_rigi);
                }
                else {
                    playerRigi.AddForce(new Vector2(Random.Range(-1,1), Random.Range(-1, 1)).normalized * knockForce + playerRigi.velocity, ForceMode2D.Force);
                    print("rigi no encontrado");
                }
            }
        }
    }

    void knock(Rigidbody2D _rigi) {
        playerRigi.AddForce(_rigi.velocity.normalized * Vector2.left * knockForce + playerRigi.velocity, ForceMode2D.Force);
        print("rigi encontrado");
    }

    public void Invulnerable()
    {
        if (canTakeDamage)
        {
            canTakeDamage = false;
            //Cambiar de layer todos los colliders del jugador
            gameObject.layer = 10;
            interactCollider.layer = 10;
            transitionCheck.layer = 10;
        }
    }

    public void Die()
    {
        //Pos te mueres
        dieReaccion?.Play(transform.position);
        CameraShaker.instance.ShakeLargo();
        Debug.Log("YOU DIE");
        StartCoroutine(DieAnim());
        FMODUnity.RuntimeManager.PlayOneShot("event:/playerSounds/die", transform.position);
    }

    IEnumerator DieAnim()
    {
        GameManager.Instance.stopPlayer = true;
        death = true;
        GameManager.Instance.player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GameManager.Instance.player.GetComponent<PlayerMovement>().horizontal = Vector2.zero;
        GameManager.Instance.player.GetComponent<Animator>().SetBool("running", false);
        GameManager.Instance.player.GetComponent<Animator>().SetBool("agacharse", false);
        GameManager.Instance.player.GetComponent<Animator>().SetBool("falling", false);
        animator.SetTrigger("Die");
        //yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        yield return new WaitForSeconds(1.0f);
        DataManager.instance.LoadData(DataManager.saveSlot);
        LevelLoader.instance.LoadScene("Game");
    }

    private void OnEnable()
    {
        GameManager.Instance.stopPlayer = false;
        death = false;
    }
}
