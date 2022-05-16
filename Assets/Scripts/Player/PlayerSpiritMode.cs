using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpiritMode : MonoBehaviour
{
    public bool spirited = false;
    public Rigidbody2D rigi;
    public Animator anim;
    private GameManager GM;
    public PlayerSpiritRange range;
    public PlayerJump playerJump;
    private float horizontalSelect;
    [SerializeField] public GameObject selectedEnemy;
    [SerializeField] public GameObject possessedEnemy;
    [SerializeField] public int selectedEnemyIndex = -1;
    PlayerSpiritAbilityHolder holder;
    private PlayerHealth health;

    public BoxCollider2D collider1;
    public BoxCollider2D collider2;
    public BoxCollider2D collider3;

    public float spiritCoolDown = 0.1f;
    public float T = 0.1f;
    FMOD.Studio.EventInstance dashSound;


    private void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        range = GetComponentInChildren<PlayerSpiritRange>();
        GM = GameObject.FindObjectOfType<GameManager>();
        holder = GetComponent<PlayerSpiritAbilityHolder>();
        health = GetComponent<PlayerHealth>();
        playerJump = GetComponent<PlayerJump>();
        T = spiritCoolDown;
    }

    private void Update()
    {
        /*
        if (GameManager.Instance.playerStats.currentMP <= 0) {
            UnsetSpirited();
        }
        */
    }

    private void LateUpdate()
    {
        if (spirited) {
            playerJump.DisLand();
        }
        if (T < spiritCoolDown) {
            T += 1 * Time.deltaTime;
        }
    }

    public void teletransportar(Transform enemy_transfor)
    {
        if (spirited)
        {
            transform.position = new Vector3(enemy_transfor.position.x, enemy_transfor.position.y, transform.position.z);
        }
    }

    public void SetSpirited()
    {
        T = 0;
        anim.SetBool("spirited_out", false);
        anim.SetTrigger("SpiritIn");
        rigi.velocity = Vector2.zero;
        rigi.constraints = RigidbodyConstraints2D.FreezeAll;
        anim.SetTrigger("spirited");
        //anim.SetBool("canSpirit", false);
        FMODUnity.RuntimeManager.PlayOneShot("event:/playerSounds/activateSpirit", transform.position);
        Invoke("AsignarEnemigoSeleccionado", 0.1f);
        playerJump.DisLand();
        //GM.StopTime();
    }

    public void UnsetSpirited()
    {
        //cambiar material del enemigo
        material_manager script;
        if (selectedEnemy)
        {
            script = selectedEnemy.GetComponentInChildren<material_manager>();
            if (script == null)
            {
                script = selectedEnemy.GetComponentInParent<material_manager>();
            }
            script.DesActivarMaterialEspiritu();
        }

        for (int i = 0; i < range.enemigosEnRango.Count; i++)
        {
            script = range.enemigosEnRango[i].GetComponentInChildren<material_manager>();
            if (script == null)
            {
                script = range.enemigosEnRango[i].GetComponentInParent<material_manager>();
            }

            script.DesActivarMaterialEspiritu();
        }
        //
        range.enemigosEnRango.Clear();
        range.enemigosAcomodados.Clear();
        spirited = false;
        rigi.constraints = RigidbodyConstraints2D.FreezeRotation;
        rigi.velocity = Vector2.down;
        GM.ResumeTime();
        if (possessedEnemy != null)
        {
            if (possessedEnemy.CompareTag("PossessionPoint") || selectedEnemy.CompareTag("Boss1PossessionPoint"))
            {
                possessedEnemy.GetComponent<PossessionPoints>().poseido = false;
            }
            else
            {
                possessedEnemy.GetComponent<EnemyBase>().poseido = false;
            }
        }
        selectedEnemy = null;
        possessedEnemy = null;
        FMODUnity.RuntimeManager.PlayOneShot("event:/playerSounds/deactivateSpirit", transform.position);
        range.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        anim.SetBool("spirited_out", true);
        playerJump.DisLand();
    }

    /*
    public void Spirit(InputAction.CallbackContext context)
    {
        if(isActiveAndEnabled)
        {
            if (context.performed)
            {
                selectedEnemyIndex = 0;
                spirited = !spirited;
                if (range.enemigosEnRango.Count > 0)
                {
                    range.OrdenarEnemigos();
                    target.SetActive(true);
                    AsignarEnemigoSeleccionado();
                }
            }
            if (spirited)
            {
                SetSpirited();
            }
            else
            {
                UnsetSpirited();
            }
        }
    }
    */

    public void SelectEnemyL(InputAction.CallbackContext context)
    {
        if (isActiveAndEnabled)
        {
            if (context.performed && spirited && range.enemigosEnRango.Count > 0)//Get Key Down
            {
                if (selectedEnemyIndex - 1 != -1)
                {
                    selectedEnemyIndex--;
                    AsignarEnemigoSeleccionado();
                }
                else
                {
                    selectedEnemyIndex = range.enemigosEnRango.Count - 1;
                    AsignarEnemigoSeleccionado();
                }
            }
        }
    }

    public void SelectEnemyR(InputAction.CallbackContext context)
    {
        if (isActiveAndEnabled)
        {
            if (context.performed && spirited && range.enemigosEnRango.Count > 0)//Get Key Down
            {
                if (selectedEnemyIndex + 1 < range.enemigosEnRango.Count)
                {
                    selectedEnemyIndex++;
                    AsignarEnemigoSeleccionado();
                }
                else
                {
                    selectedEnemyIndex = 0;
                    AsignarEnemigoSeleccionado();
                }
            }
        }
    }

    public void Teleport(InputAction.CallbackContext context)
    {
        if (isActiveAndEnabled)
        {
            if (context.performed && spirited && selectedEnemy && selectedEnemyIndex>=0 && GameManager.Instance.playerStats.currentMP >= GameManager.Instance.playerStats.teletransportationCost && T >= spiritCoolDown)//Get Key Down
            {
                T = 0;
                health.Invulnerable();
                if (possessedEnemy != null)
                {
                    if (possessedEnemy.CompareTag("Enemy"))
                    {
                        possessedEnemy.GetComponent<EnemyBase>().poseido = false;
                    }
                    else if (possessedEnemy.CompareTag("PossessionPoint") || selectedEnemy.CompareTag("Boss1PossessionPoint"))
                    {
                        possessedEnemy.GetComponent<PossessionPoints>().poseido = false;
                    }
                }
                possessedEnemy = selectedEnemy;
                material_manager script = null;
                script = selectedEnemy.GetComponentInChildren<material_manager>();
                if (script == null)
                {
                    script = selectedEnemy.GetComponentInParent<material_manager>();
                }

                script.DesActivarMaterialEspiritu();

                transform.position = selectedEnemy.transform.position;
                GameManager.Instance.playerStats.currentMP -= GameManager.Instance.playerStats.teletransportationCost;//costo en mp del la habilidad
                if (selectedEnemy.CompareTag("Enemy"))
                {
                    EnemyBase enemyScript = selectedEnemy.GetComponent<EnemyBase>();
                    enemyScript.poseido = true;
                }
                else if (selectedEnemy.CompareTag("PossessionPoint") || selectedEnemy.CompareTag("Boss1PossessionPoint"))
                {
                    selectedEnemy.GetComponent<PossessionPoints>().poseido = true;
                }

                if (selectedEnemyIndex - 1 != -1)
                {
                    selectedEnemyIndex--;
                    AsignarEnemigoSeleccionado();
                }
                else
                {
                    selectedEnemyIndex = range.enemigosEnRango.Count - 1;
                    AsignarEnemigoSeleccionado();
                }
                PlayDashEvent();
                playerJump.DisLand();
            }
        }
    }

    public void SpiritKill(InputAction.CallbackContext context)//habilidad para matar enemigo
    {
        if (isActiveAndEnabled)
        {
            if (context.performed && spirited && selectedEnemy && selectedEnemyIndex >= 0 && GameManager.Instance.playerStats.currentMP >= GameManager.Instance.playerStats.teletransportationCost* 2 && T >= spiritCoolDown)//Get Key Down
            {
                T = 0;
                health.Invulnerable();
                CameraShaker.instance.ShakeCorto();
                if (possessedEnemy != null)
                {
                    if (possessedEnemy.CompareTag("Enemy"))
                    {
                        possessedEnemy.GetComponent<EnemyBase>().poseido = false;
                    }
                    else if (possessedEnemy.CompareTag("PossessionPoint") || selectedEnemy.CompareTag("Boss1PossessionPoint"))
                    {
                        possessedEnemy.GetComponent<PossessionPoints>().poseido = false;
                    }
                }
                possessedEnemy = selectedEnemy;
                material_manager script = null;
                script = selectedEnemy.GetComponentInChildren<material_manager>();
                if (script == null)
                {
                    script = selectedEnemy.GetComponentInParent<material_manager>();
                }

                script.DesActivarMaterialEspiritu();

                EnemyBase enemyScript = selectedEnemy.GetComponent<EnemyBase>();
                transform.position = selectedEnemy.transform.position;
                if (selectedEnemy.CompareTag("Enemy"))
                {
                    if (enemyScript.currentHealth < 1000)
                    {
                        enemyScript.TakeDamage(enemyScript.currentHealth);
                        enemyScript.gameObject.GetComponentInChildren<EnemyHurtReactions>().getHurt();
                    }
                    enemyScript.poseido = true;
                }
                else if (selectedEnemy.CompareTag("PossessionPoint") || selectedEnemy.CompareTag("Boss1PossessionPoint"))
                {
                    selectedEnemy.GetComponent<PossessionPoints>().poseido = true;
                }
                GameManager.Instance.playerStats.currentMP -= GameManager.Instance.playerStats.teletransportationCost * 2;//costo en mp del la habilidad

                if (selectedEnemyIndex + 1 < range.enemigosEnRango.Count)
                {
                    selectedEnemyIndex++;
                    AsignarEnemigoSeleccionado();
                }
                else
                {
                    selectedEnemyIndex = 0;
                    AsignarEnemigoSeleccionado();
                }
                PlayDashEvent();
                playerJump.DisLand();
            }
        }

    }

    public void AsignarEnemigoSeleccionado()
    {
        if (range.enemigosEnRango.Count > 0)
        {
            if (range.enemigosEnRango[selectedEnemyIndex])
            {
                material_manager script = null;
                //cambiar material del enemigo
                selectedEnemy = range.enemigosEnRango[selectedEnemyIndex];
                script = selectedEnemy.GetComponentInChildren<material_manager>();
                if (script == null)
                {
                    selectedEnemy.GetComponentInParent<material_manager>();
                }
                script.ActivarMaterialEspiritu();

                //apagar materiales de los otros enemigos
                for (int i = 0; i < range.enemigosEnRango.Count; i++)
                {
                    if (i == selectedEnemyIndex) continue;
                    script = range.enemigosEnRango[i].GetComponentInChildren<material_manager>();
                    if (script == null)
                    {
                        range.enemigosEnRango[i].GetComponentInParent<material_manager>();
                    }
                    script.DesActivarMaterialEspiritu();
                }
            }
        }
    }

    public void ToggleCanSpiritOn()
    {
       anim.SetBool("canSpirit", true);
    }

    public void ToggleCanSpiritOff()
    {
        anim.SetBool("canSpirit", false);
    }

    public void PlayDashEvent()
    {
        dashSound = FMODUnity.RuntimeManager.CreateInstance("event:/playerSounds/dashSpirit");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(dashSound, transform);
        dashSound.start();
        dashSound.release();
    }
}

