using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;

//Clase con todas las listas que se guardaran, usé un archivo de guardado diferente porque no se puede encriptar un json
[Serializable]
public class SavedGameProgress
{
    public List<Items> HPIncreaseItems;
    public List<Items> MPIncreaseItems;
    public List<Items> SPRangeIncreaseItems;
    public List<Items> RangeIncreaseItems;
    public List<Items> Rooms;
}

[Serializable]
public class Items
{
    public int id;
    public bool isActive;
}

public class GameProgress : MonoBehaviour
{
    SavedGameProgress savedProgress = new SavedGameProgress()
    {
        HPIncreaseItems = new List<Items>(),
        MPIncreaseItems = new List<Items>(),
        SPRangeIncreaseItems = new List<Items>(),
        RangeIncreaseItems = new List<Items>(),
        Rooms = new List<Items>()
    };
    Items items;
    List<Items> itemsMap = new List<Items>();

    //Listas de items en el mapa
    public List<ItemIncreaseMP> quantityMPIncreaseItems; 
    public List<ItemIncreaseHP> quantityHPIncreaseItems; 
    public List<ItemIncreaseSpiritRange> quantitySPRangeIncreaseItems; 
    public List<ItemIncreaseBulletRange> quantityRangeIncreaseItems;
    public List<RoomData> rooms = new List<RoomData>();
    //Items unicos
    public GameObject doubleJump;
    public GameObject spiritAbility;
    //Lista de rooms UI (Mapa)
    public List<GameObject> roomsUI = new List<GameObject>();
    //Numero de habitaciones descubiertas
    public int discoveredRooms;
    //Lista de esencias, osea el inventario del jugador--------------------------------------------------------------------------------------------------------
    [Header("Inventory")]
    public List<CombatEssence> combatEssences = new List<CombatEssence>();
    public List<SpiritEssence> spiritEssences = new List<SpiritEssence>();
    public List<PassiveEssence> passiveEssences = new List<PassiveEssence>();
    //UI del inventario
    [Header("Inventory UI")]
    public GameObject prefabEssenceButton;
    public GameObject passiveEssenceListUI;
    public GameObject combatEssenceListUI;
    public GameObject spiritEssenceListUI;

    public void AddEssence(Essence _essence)
    {
        int _quantity = 1;

        if (_essence.type == essenceType.Combat)
        {
            //Revisar si ya tiene una esencia igual y cuantas tiene
            for (int i = 0; i < combatEssences.Count; i++)
            {
                if (_essence.combatObject.name == combatEssences[i].name)
                {
                    _quantity++;
                    _essence.combatObject.UIButton = combatEssences[i].UIButton;
                }
            }
            //Si es la primera que consigue se añade el boton
            if (_quantity == 1)
            {
                GameManager.Instance.itemsCollected++; //Añadir 1 al numero de items recolectados pero solo si en nuevo
                _essence.combatObject.UIButton = Instantiate(prefabEssenceButton, combatEssenceListUI.transform);
                _essence.combatObject.UIButton.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _essence.combatObject.name;
                _essence.combatObject.UIButton.GetComponent<InventoryEssenceBtn>().name = _essence.combatObject.name;
                _essence.combatObject.UIButton.GetComponent<InventoryEssenceBtn>().description = _essence.combatObject.description;
                _essence.combatObject.UIButton.GetComponent<InventoryEssenceBtn>().path = _essence.combatObject.path;
                _essence.combatObject.UIButton.GetComponent<InventoryEssenceBtn>().quantity = _quantity;
                combatEssences.Add(_essence.combatObject);
            }
            else if (_quantity > 1 && _quantity <= 3) //Si tiene mas de una solo aumenta el numero mostrado en el inventario
            {
                _essence.combatObject.UIButton.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = _quantity.ToString();
                _essence.combatObject.UIButton.GetComponent<InventoryEssenceBtn>().quantity = _quantity;
                combatEssences.Add(_essence.combatObject);
            }
            else //No puedes tener mas de tres 
            {
                Debug.Log("No puedes tener mas de 3 esencias iguales");
            }
        }
        else if (_essence.type == essenceType.Passive)
        {
            //Revisar si ya tiene una esencia igual y cuantas tiene
            for (int i = 0; i < passiveEssences.Count; i++)
            {
                if (_essence.passiveObject.name == passiveEssences[i].name)
                {
                    _quantity++;
                    _essence.passiveObject.UIButton = passiveEssences[i].UIButton;
                }
            }
            //Si es la primera que consigue se añade el boton
            if (_quantity == 1)
            {
                GameManager.Instance.itemsCollected++; //Añadir 1 al numero de items recolectados pero solo si en nuevo
                _essence.passiveObject.UIButton = Instantiate(prefabEssenceButton, passiveEssenceListUI.transform);
                _essence.passiveObject.UIButton.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _essence.passiveObject.name;
                _essence.passiveObject.UIButton.GetComponent<InventoryEssenceBtn>().name = _essence.passiveObject.name;
                _essence.passiveObject.UIButton.GetComponent<InventoryEssenceBtn>().description = _essence.passiveObject.description;
                _essence.passiveObject.UIButton.GetComponent<InventoryEssenceBtn>().path = _essence.passiveObject.path;
                _essence.passiveObject.UIButton.GetComponent<InventoryEssenceBtn>().quantity = _quantity;
                passiveEssences.Add(_essence.passiveObject);
            }
            else if (_quantity > 1 && _quantity <= 3) //Si tiene mas de una solo aumenta el numero mostrado en el inventario
            {
                _essence.passiveObject.UIButton.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = _quantity.ToString();
                _essence.passiveObject.UIButton.GetComponent<InventoryEssenceBtn>().quantity = _quantity;
                passiveEssences.Add(_essence.passiveObject);
            }
            else //No puedes tener mas de tres 
            {
                Debug.Log("No puedes tener mas de 3 esencias iguales");
            }
        }
        else if (_essence.type == essenceType.Spirit)
        {
            //Revisar si ya tiene una esencia igual y cuantas tiene
            for (int i = 0; i < spiritEssences.Count; i++)
            {
                if (_essence.spiritObject.name == spiritEssences[i].name)
                {
                    _quantity++;
                    _essence.spiritObject.UIButton = spiritEssences[i].UIButton;
                }
            }
            //Si es la primera que consigue se añade el boton
            if (_quantity == 1)
            {
                GameManager.Instance.itemsCollected++; //Añadir 1 al numero de items recolectados pero solo si en nuevo
                _essence.spiritObject.UIButton = Instantiate(prefabEssenceButton, spiritEssenceListUI.transform);
                _essence.spiritObject.UIButton.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _essence.spiritObject.name;
                _essence.spiritObject.UIButton.GetComponent<InventoryEssenceBtn>().name = _essence.spiritObject.name;
                _essence.spiritObject.UIButton.GetComponent<InventoryEssenceBtn>().description = _essence.spiritObject.description;
                _essence.spiritObject.UIButton.GetComponent<InventoryEssenceBtn>().path = _essence.spiritObject.path;
                _essence.spiritObject.UIButton.GetComponent<InventoryEssenceBtn>().quantity = _quantity;
                spiritEssences.Add(_essence.spiritObject);
            }
            else if (_quantity > 1 && _quantity <= 3) //Si tiene mas de una solo aumenta el numero mostrado en el inventario
            {
                _essence.spiritObject.UIButton.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = _quantity.ToString();
                _essence.spiritObject.UIButton.GetComponent<InventoryEssenceBtn>().quantity = _quantity;
                spiritEssences.Add(_essence.spiritObject);
            }
            else //No puedes tener mas de tres 
            {
                Debug.Log("No puedes tener mas de 3 esencias iguales");
            }
        }
    }
    //---------------------------------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            itemsMap.Add(new Items());
        }
    }

    private void Start()
    {        
        CargarListas();
    }

    public void GuardarListas()
    {
        //Items de HP
        for(int i = 0; i < quantityHPIncreaseItems.Count; i++)
        {
            items = new Items();
            items.id = quantityHPIncreaseItems[i].id;
            items.isActive = quantityHPIncreaseItems[i].gameObject.activeInHierarchy;

            savedProgress.HPIncreaseItems.Add(items);
        }

        //Items de MP
        for (int i = 0; i < quantityMPIncreaseItems.Count; i++)
        {
            items = new Items();
            items.id = quantityMPIncreaseItems[i].id;
            items.isActive = quantityMPIncreaseItems[i].gameObject.activeInHierarchy;

            savedProgress.MPIncreaseItems.Add(items);
        }

        //Items Spirit Range
        for (int i = 0; i < quantitySPRangeIncreaseItems.Count; i++)
        {
            items = new Items();
            items.id = quantitySPRangeIncreaseItems[i].id;
            items.isActive = quantitySPRangeIncreaseItems[i].gameObject.activeInHierarchy;

            savedProgress.SPRangeIncreaseItems.Add(items);
        }

        //Items Shooting Range
        for (int i = 0; i < quantityRangeIncreaseItems.Count; i++)
        {
            items = new Items();
            items.id = quantityRangeIncreaseItems[i].id;
            items.isActive = quantityRangeIncreaseItems[i].gameObject.activeInHierarchy;

            savedProgress.RangeIncreaseItems.Add(items);
        }

        CheckDiscoveredRooms();
        DataManager.instance.discoveredRooms = discoveredRooms;
        //Guardar datos en un json
        string json = JsonUtility.ToJson(savedProgress);

        string path = Application.persistentDataPath + "/gameprogress_" + DataManager.saveSlot.ToString() + ".bin";

        File.WriteAllText(path, json);
    }

    public void CheckDiscoveredRooms()
    {
        discoveredRooms = 0;
        //Rooms
        for (int i = 0; i < rooms.Count; i++)
        {
            itemsMap[i].id = rooms[i].id;
            itemsMap[i].isActive = rooms[i].visited;

            if (rooms[i].visited)
            {
                discoveredRooms++;
            }

            savedProgress.Rooms.Add(itemsMap[i]);
        }
    }

    public void CargarListas()
    {
        if(DataManager.instance)
        {
            string path = Application.persistentDataPath + "/gameprogress_" + DataManager.saveSlot.ToString() + ".bin";

            doubleJump.SetActive(!DataManager.instance.doubleJump);
            spiritAbility.SetActive(!DataManager.instance.spiritAbility);

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);

                SavedGameProgress loadedSavedProgress = JsonUtility.FromJson<SavedGameProgress>(json);
                //HP items
                for (int i = 0; i < quantityHPIncreaseItems.Count; i++)
                {
                    for (int j = 0; j < loadedSavedProgress.HPIncreaseItems.Count; j++)
                    {
                        if (quantityHPIncreaseItems[i].id == loadedSavedProgress.HPIncreaseItems[j].id)
                        {
                            quantityHPIncreaseItems[i].gameObject.SetActive(loadedSavedProgress.HPIncreaseItems[j].isActive);
                        }
                    }
                }
                //MP items
                for (int i = 0; i < quantityMPIncreaseItems.Count; i++)
                {
                    for (int j = 0; j < loadedSavedProgress.MPIncreaseItems.Count; j++)
                    {
                        if (quantityMPIncreaseItems[i].id == loadedSavedProgress.MPIncreaseItems[j].id)
                        {
                            quantityMPIncreaseItems[i].gameObject.SetActive(loadedSavedProgress.MPIncreaseItems[j].isActive);
                        }
                    }
                }
                //Spirit items
                for (int i = 0; i < quantitySPRangeIncreaseItems.Count; i++)
                {
                    for (int j = 0; j < loadedSavedProgress.SPRangeIncreaseItems.Count; j++)
                    {
                        if (quantitySPRangeIncreaseItems[i].id == loadedSavedProgress.SPRangeIncreaseItems[j].id)
                        {
                            quantitySPRangeIncreaseItems[i].gameObject.SetActive(loadedSavedProgress.SPRangeIncreaseItems[j].isActive);
                        }
                    }
                }
                //Shooting items
                for (int i = 0; i < quantityRangeIncreaseItems.Count; i++)
                {
                    for (int j = 0; j < loadedSavedProgress.RangeIncreaseItems.Count; j++)
                    {
                        if (quantityRangeIncreaseItems[i].id == loadedSavedProgress.RangeIncreaseItems[j].id)
                        {
                            quantityRangeIncreaseItems[i].gameObject.SetActive(loadedSavedProgress.RangeIncreaseItems[j].isActive);
                        }
                    }
                }
                //Revisar cuales room ya has visitado
                for (int i = 0; i < rooms.Count; i++)
                {
                    roomsUI[i].SetActive(loadedSavedProgress.Rooms[i].isActive);
                    rooms[i].visited = loadedSavedProgress.Rooms[i].isActive;
                }
            }
        }
    }
}
