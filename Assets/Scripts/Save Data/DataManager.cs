using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    //Numero de items en total
    public int totalItems;

    //Datos a guardar
    [Header("Saved Data")]
    public float playerPositionX;
    public float playerPositionY;
    public string areaName;
    public float time;
    //Cuartos explorados
    public int discoveredRooms;
    public int itemsCollected; //Porcentaje de items obtenidos

    [Header("Auras")]
    public bool blue;
    public bool green;
    public bool red;

    [Header("Items unicos")]
    //Items unicos
    public bool doubleJump;
    public bool spiritAbility;

    [Header("Player Stats")]
    public int maxHealth = 10;
    /*public int maxMP; 
    public float bulletRange;
    public float spiritRange;*/

    [Header("Inventory")]
    //Guardar inventario
    public List<string> inventoryPassive = new List<string>();
    public List<string> inventorySpirit = new List<string>();
    public List<string> inventoryCombat = new List<string>();
    //Guardar Loadouts
    public string blueCombatEssence;
    public string blueSpiritEssence;
    public List<string> bluePassiveEssence = new List<string>();

    public string greenCombatEssence;
    public string greenSpiritEssence;
    public List<string> greenPassiveEssence = new List<string>();

    public string redCombatEssence;
    public string redSpiritEssence;
    public List<string> redPassiveEssence = new List<string>();

    [Header("Bosses Defeated")]
    public bool boss1;
    public bool boss2;
    public bool boss3;

    //Numero de slot que se cargo al iniciar partida
    public static int saveSlot;

    [Header("Settings")]
    public float BgmVolume = 1;
    public float SfxVolume = 1;
    public int currentResolutionHeight;
    public int currentResolutionWidth;
    public int currentResolutionIndex;

    void Start()
    {
        currentResolutionHeight = 1080;
        currentResolutionWidth = 1920;
        currentResolutionIndex = 4;

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        if (SaveSystem.CheckSettingsFileExist())
        {
            LoadSettings();
            Screen.SetResolution(currentResolutionWidth, currentResolutionHeight, currentResolutionWidth == 1920);
        }
    }

    public void LoadData(int _slot)
    {
        saveSlot = _slot;

        if (SaveSystem.CheckFileExist(_slot))
        {
            Data data = SaveSystem.LoadData(_slot);

            //maxHealth = data.maxHealth;
            playerPositionX = data.playerPositionX;
            playerPositionY = data.playerPositionY;
            areaName = data.areaName;
            time = data.time;
            /*maxMP = data.maxMP;
            bulletRange = data.bulletRange;
            spiritRange = data.spiritRange;*/
            discoveredRooms = data.discoveredRooms;

            inventoryPassive = data.inventoryPassive;
            inventorySpirit = data.inventorySpirit;
            inventoryCombat = data.inventoryCombat;

            //Loadouts
            blueCombatEssence = data.blueCombatEssence;
            blueSpiritEssence = data.blueSpiritEssence;
            bluePassiveEssence = data.bluePassiveEssence;

            greenCombatEssence = data.greenCombatEssence;
            greenSpiritEssence = data.greenSpiritEssence;
            greenPassiveEssence = data.greenPassiveEssence;

            redCombatEssence = data.redCombatEssence;
            redSpiritEssence = data.redSpiritEssence;
            redPassiveEssence = data.redPassiveEssence;

            boss1 = data.boss1;
            boss2 = data.boss2;
            boss3 = data.boss3;

            blue = data.blue;
            green = data.green;
            red = data.red;

            doubleJump = data.doubleJump;
            spiritAbility = data.spiritAbility;

            itemsCollected = data.itemsCollected;
        }
    }

    public void SaveData(int _slot)
    {
        saveSlot = _slot;
        SaveSystem.SaveData(_slot, this);
    }

    public void DeleteData(int _slot)
    {
        SaveSystem.DeleteData(_slot);
    }

    public void SaveSettings()
    {
        SaveSystem.SaveSettings(this);
    }

    public void LoadSettings()
    {
        Settings settings = SaveSystem.LoadSettings();

        BgmVolume = settings.BgmVolume;
        SfxVolume = settings.SfxVolume;
        currentResolutionHeight = settings.currentResolutionHeight;
        currentResolutionWidth = settings.currentResolutionWidth;
        currentResolutionIndex = settings.currentResolutionIndex;
    }
}
