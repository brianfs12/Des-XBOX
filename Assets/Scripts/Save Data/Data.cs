//Azael Sanchez
//Clase que contiene los datos a guardar
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    //Datos a guardar
    [SerializeField]
    public int maxHealth; //Vida maxima del jugador
    public float playerPositionX; //Posicion X del jugador
    public float playerPositionY; //Posicion Y del jugador
    public string areaName; //Nombre del area donde guardaste
    public float time; //Tiempo de partida 
    public int discoveredRooms; //Numero de habitaciones descubiertas
    public int itemsCollected; //Porcentaje de items obtenidos
    //Items unicos
    public bool doubleJump;
    public bool spiritAbility;
    //Inventario
    public List<string> inventoryPassive = new List<string>();
    public List<string> inventorySpirit = new List<string>();
    public List<string> inventoryCombat = new List<string>();
    //Loadouts
    public string blueCombatEssence;
    public string blueSpiritEssence;
    public List<string> bluePassiveEssence = new List<string>();

    public string greenCombatEssence;
    public string greenSpiritEssence;
    public List<string> greenPassiveEssence = new List<string>();

    public string redCombatEssence;
    public string redSpiritEssence;
    public List<string> redPassiveEssence = new List<string>();

    //Habilidades permanentes
    public int maxMP;
    public float bulletRange;
    public float spiritRange;

    //Bosses derrotados
    public bool boss1;
    public bool boss2;
    public bool boss3;

    //Auras
    public bool blue, green, red;

    public Data(DataManager manager)
    {
        //maxHealth = manager.maxHealth;
        playerPositionX = manager.playerPositionX;
        playerPositionY = manager.playerPositionY;
        areaName = manager.areaName;
        time = manager.time;
        /*maxMP = manager.maxMP;
        bulletRange = manager.bulletRange;
        spiritRange = manager.spiritRange;*/
        discoveredRooms = manager.discoveredRooms;

        inventoryPassive = manager.inventoryPassive;
        inventorySpirit = manager.inventorySpirit;
        inventoryCombat = manager.inventoryCombat;

        blueCombatEssence = manager.blueCombatEssence;
        blueSpiritEssence = manager.blueSpiritEssence;
        bluePassiveEssence = manager.bluePassiveEssence;
        greenCombatEssence = manager.greenCombatEssence;
        greenSpiritEssence = manager.greenSpiritEssence;
        greenPassiveEssence = manager.greenPassiveEssence;
        redCombatEssence = manager.redCombatEssence;
        redSpiritEssence = manager.redSpiritEssence;
        redPassiveEssence = manager.redPassiveEssence;

        boss1 = manager.boss1;
        boss2 = manager.boss2;
        boss3 = manager.boss3;

        blue = manager.blue;
        green = manager.green;
        red = manager.red;

        doubleJump = manager.doubleJump;
        spiritAbility = manager.spiritAbility;

        itemsCollected = manager.itemsCollected;
    }
}
