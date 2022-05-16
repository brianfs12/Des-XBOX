using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayMaker;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private Animator anim;
    [Header("Player things")]
    public PlayerShoot playerShoot;
    public PlayerRange playerRange;
    public PlayerSpiritRange playerSPRange;
    public PlayerStats playerStats;
    public PlayerAuraLoadouts playerLoadouts;
    public GameProgress gameProgress;
    public static GameManager Instance;
    public bool pause = false;
    public bool stopPlayer = false; //Detener las acciones del jugador
    public PlayMakerFSM introCutscene;

    [HideInInspector]
    public bool canPause;

    //Contador de tiempo de partida
    public float time;

    //Todos los items del juego
    [Header("Lists of all essences in game")]
    public List<Essence> passive = new List<Essence>();
    public List<Essence> spirit = new List<Essence>();
    public List<Essence> combat = new List<Essence>();

    public bool isOnBlueZone;
    public bool isOnGreenZone;
    public bool isOnRedZone;
    public bool isOnSafeZone;

    public bool boss1;
    public bool boss2;
    public bool boss3;

    public bool red, blue, green;

    public bool doubleJump;
    public bool spiritAbility;
    public int itemsCollected;

    /*[Header("List of all the rest of items")]
    public List<>*/

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
        playerShoot = player.GetComponent<PlayerShoot>();
        playerStats = player.GetComponent<PlayerStats>();
        playerLoadouts = player.GetComponent<PlayerAuraLoadouts>();
        canPause = true;
    }

    void Start()
    {
        anim = player.GetComponent<Animator>();

        //Iniciar jugador con los datos de guardado
        if(DataManager.instance)
        {
            player.transform.position = new Vector3(DataManager.instance.playerPositionX, DataManager.instance.playerPositionY, 0.0f);
            //playerStats.maxHealth = DataManager.instance.maxHealth;
            /*playerStats.maxMP = DataManager.instance.maxMP;
            playerStats.spiritRange = DataManager.instance.spiritRange;
            playerStats.bulletRange = DataManager.instance.bulletRange;*/

            itemsCollected = DataManager.instance.itemsCollected;
            spiritAbility = DataManager.instance.spiritAbility;
            doubleJump = DataManager.instance.doubleJump;
            boss1 = DataManager.instance.boss1;
            boss2 = DataManager.instance.boss2;
            boss3 = DataManager.instance.boss3;
            blue = DataManager.instance.blue;
            green = DataManager.instance.green;
            red = DataManager.instance.red;
        }

        if(doubleJump)
        {
            player.GetComponent<PlayerJump>().maxJumpTimes = 2;
            player.GetComponent<PlayerJump>().jumpTimes = 2;
        }

        //Iniciar contador con tiempo guardado
        if(DataManager.instance)
        {
            time = DataManager.instance.time;
        }

        # region Instanciar los botones del inventario guardado
        if(DataManager.instance)
        {
            for (int i = 0; i < DataManager.instance.inventoryPassive.Count; i++)
            {
                for (int j = 0; j < passive.Count; j++)
                {
                    if (passive[j].passiveObject != null && passive[j].type == essenceType.Passive && DataManager.instance.inventoryPassive[i] == passive[j].passiveObject.name)
                    {
                        gameProgress.AddEssence(passive[j]);
                    }
                }
            }
            for (int i = 0; i < DataManager.instance.inventorySpirit.Count; i++)
            {
                for (int j = 0; j < spirit.Count; j++)
                {
                    if (spirit[j].spiritObject != null && spirit[j].type == essenceType.Spirit && DataManager.instance.inventorySpirit[i] == spirit[j].spiritObject.name)
                    {
                        gameProgress.AddEssence(spirit[j]);
                    }
                }
            }
            for (int i = 0; i < DataManager.instance.inventoryCombat.Count; i++)
            {
                for (int j = 0; j < combat.Count; j++)
                {
                    if (combat[j].combatObject != null && combat[j].type == essenceType.Combat && DataManager.instance.inventoryCombat[i] == combat[j].combatObject.name)
                    {
                        gameProgress.AddEssence(combat[j]);
                    }
                }
            }
        }
        #endregion

        #region Equipar esencias guardadas
        if(DataManager.instance)
        {
            if (DataManager.instance.blueSpiritEssence != "")
            {
                for (int i = 0; i < spirit.Count; i++)
                {
                    if (spirit[i].spiritObject.name == DataManager.instance.blueSpiritEssence)
                    {
                        playerLoadouts.StartEquipBlueSpiritEssences(spirit[i].spiritObject.UIButton.GetComponent<InventoryEssenceBtn>());
                    }
                }
            }

            if (DataManager.instance.blueCombatEssence != "")
            {
                for (int i = 0; i < combat.Count; i++)
                {
                    if (combat[i].combatObject.name == DataManager.instance.blueCombatEssence)
                    {
                        playerLoadouts.StartEquipBlueCombatEssences(combat[i].combatObject.UIButton.GetComponent<InventoryEssenceBtn>());
                    }
                }
            }

            for (int i = 0; i < DataManager.instance.bluePassiveEssence.Count; i++)
            {
                for (int j = 0; j < passive.Count; j++)
                {
                    if (passive[j].passiveObject != null && passive[j].type == essenceType.Passive && DataManager.instance.bluePassiveEssence[i] == passive[j].passiveObject.name)
                    {
                        playerLoadouts.StartEquipBluePassiveEssences(passive[j].passiveObject.UIButton.GetComponent<InventoryEssenceBtn>());
                    }
                }
            }

            if (DataManager.instance.greenSpiritEssence != "")
            {
                for (int i = 0; i < spirit.Count; i++)
                {
                    if (spirit[i].spiritObject.name == DataManager.instance.greenSpiritEssence)
                    {
                        playerLoadouts.StartEquipGreenSpiritEssences(spirit[i].spiritObject.UIButton.GetComponent<InventoryEssenceBtn>());
                    }
                }
            }

            if (DataManager.instance.greenCombatEssence != "")
            {
                for (int i = 0; i < combat.Count; i++)
                {
                    if (combat[i].combatObject.name == DataManager.instance.greenCombatEssence)
                    {
                        playerLoadouts.StartEquipGreenCombatEssences(combat[i].combatObject.UIButton.GetComponent<InventoryEssenceBtn>());
                    }
                }
            }

            for (int i = 0; i < DataManager.instance.greenPassiveEssence.Count; i++)
            {
                for (int j = 0; j < passive.Count; j++)
                {
                    if (passive[j].passiveObject != null && passive[j].type == essenceType.Passive && DataManager.instance.greenPassiveEssence[i] == passive[j].passiveObject.name)
                    {
                        playerLoadouts.StartEquipGreenPassiveEssences(passive[j].passiveObject.UIButton.GetComponent<InventoryEssenceBtn>());
                    }
                }
            }

            if (DataManager.instance.redSpiritEssence != "")
            {
                for (int i = 0; i < spirit.Count; i++)
                {
                    if (spirit[i].spiritObject.name == DataManager.instance.redSpiritEssence)
                    {
                        playerLoadouts.StartEquipRedSpiritEssences(spirit[i].spiritObject.UIButton.GetComponent<InventoryEssenceBtn>());
                    }
                }
            }

            if (DataManager.instance.redCombatEssence != "")
            {
                for (int i = 0; i < combat.Count; i++)
                {
                    if (combat[i].combatObject.name == DataManager.instance.redCombatEssence)
                    {
                        playerLoadouts.StartEquipRedCombatEssences(combat[i].combatObject.UIButton.GetComponent<InventoryEssenceBtn>());
                    }
                }
            }

            for (int i = 0; i < DataManager.instance.redPassiveEssence.Count; i++)
            {
                for (int j = 0; j < passive.Count; j++)
                {
                    if (passive[j].passiveObject != null && passive[j].type == essenceType.Passive && DataManager.instance.redPassiveEssence[i] == passive[j].passiveObject.name)
                    {
                        playerLoadouts.StartEquipRedPassiveEssences(passive[j].passiveObject.UIButton.GetComponent<InventoryEssenceBtn>());
                    }
                }
            }

            playerLoadouts.ChangeAllEssences();
        }
        #endregion
    }

    public void ResetHealthMP()
    {
        playerStats.currentHealth = playerStats.maxHealth;
        playerStats.currentMP = playerStats.maxMP;
    }

    public void StopTime() {
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void ResumeTime() {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    void Update()
    {
        //Guardar el timepo de juego en todo momento---------------------
        if(DataManager.instance)
        {
            DataManager.instance.time = time;
            DataManager.instance.SaveData(DataManager.saveSlot);
        }
        //---------------------------------------------------------------

        anim.speed = 1 / Time.timeScale;

        //Contador de tiempo de partida
        time += Time.deltaTime;
    }

    public void StopPlayer()
    {
        stopPlayer = true;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        player.GetComponent<PlayerMovement>().horizontal = Vector2.zero;
        player.GetComponent<Animator>().SetBool("running", false);
        player.GetComponent<Animator>().Rebind();
        player.GetComponent<PlayerSpiritAbilityHolder>().ForceEssenceOff();
    }

    public void MakePlayerInvisible()
    {
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<PlayerLightController>().headLight.enabled = false;
    }

    public void MakePlayerVisible()
    {
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<PlayerLightController>().headLight.enabled = true;
    }

    public void ResumePlayer()
    {
        stopPlayer = false;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void TogglePause()
    {
        if(canPause)
        {
            canPause = false;
        }
        else
        {
            canPause = true;
        }
    }

    public void SkipIntro()
    {
        if(DataManager.instance && DataManager.instance.areaName == "Train")
        {
            introCutscene.SendEvent("NotSkip");
        }
        else
        {
            MakePlayerVisible();
            introCutscene.SendEvent("Skip");
        }
    }
}
