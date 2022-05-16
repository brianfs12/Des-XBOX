using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject gamepadControls;
    public GameObject keyboardControls;
    bool change = true;
    WaitForSeconds wait = new WaitForSeconds(2.0f);
    [SerializeField]
    GameObject[] slotButtons;
    [SerializeField]
    GameObject[] emptyButtons;
    [SerializeField]
    GameObject[] shrineOn;
    [SerializeField]
    GameObject[] shrineOff;
    [SerializeField]
    GameObject[] slotPanels;
    [SerializeField]
    GameObject[] emptyPanels;
    [SerializeField]
    public TextMeshProUGUI[] areaText;
    [SerializeField]
    TextMeshProUGUI[] timeText;
    [SerializeField]
    TextMeshProUGUI[] mapText;
    [SerializeField]
    TextMeshProUGUI[] itemText;
    [SerializeField]
    GameObject[] bossFlame_1;
    [SerializeField]
    GameObject[] bossFlame_2;
    [SerializeField]
    GameObject[] bossFlame_3;

    EventSystem m_EventSystem;


    void OnEnable()
    {
        m_EventSystem = EventSystem.current;
    }

    void Start()
    {
        change = true;
        CheckFiles();   
    }

    void onInputDeviceChange(InputUser user, InputUserChange change, InputDevice device)
    {
        if (change == InputUserChange.ControlSchemeChanged)
        {
            updateButtonImage(user.controlScheme.Value.name);
        }
    }

    void updateButtonImage(string schemeName)
    {
        if (schemeName.Equals("Gamepad") && gamepadControls != null && keyboardControls != null)
        {
            gamepadControls.SetActive(true);
            keyboardControls.SetActive(false);
        }
        else if (!schemeName.Equals("Gamepad") && gamepadControls != null && keyboardControls != null)
        {
            gamepadControls.SetActive(false);
            keyboardControls.SetActive(true);
        }
    }

    IEnumerator ChangeSprite()
    {
        yield return wait;
        change = true;
    }

    private void Update()
    {
        if(change)
        {
            change = false;
            InputUser.onChange += onInputDeviceChange;
            StartCoroutine(ChangeSprite());
        }

        if (m_EventSystem.currentSelectedGameObject == slotButtons[0])
        {
            slotPanels[0].SetActive(true);
            slotPanels[1].SetActive(false);
            slotPanels[2].SetActive(false);

            emptyPanels[0].SetActive(false);
            emptyPanels[1].SetActive(false);
            emptyPanels[2].SetActive(false);

            DataManager.instance.LoadData(0);
            areaText[0].text = DataManager.instance.areaName;
            if(DataManager.instance.discoveredRooms < 10) mapText[0].text = "00" + DataManager.instance.discoveredRooms.ToString() + "%";
            else if (DataManager.instance.discoveredRooms >= 10 && DataManager.instance.discoveredRooms < 100) mapText[0].text = "0" + DataManager.instance.discoveredRooms.ToString() + "%";
            else mapText[0].text = DataManager.instance.discoveredRooms.ToString() + "%";
            var timeSpan = System.TimeSpan.FromSeconds(DataManager.instance.time);
            timeText[0].text = timeSpan.Hours.ToString("00") + ":" + timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00");
            bossFlame_1[0].SetActive(DataManager.instance.boss1);
            bossFlame_1[1].SetActive(DataManager.instance.boss2);
            bossFlame_1[2].SetActive(DataManager.instance.boss3);
            //Porcentaje de items
            int itemPer = Mathf.RoundToInt((DataManager.instance.itemsCollected * 100) / DataManager.instance.totalItems);
            if (itemPer < 10) itemText[0].text = "00" + itemPer.ToString() + "%";
            else if (itemPer >= 10 && itemPer < 100) itemText[0].text = "0" + itemPer.ToString() + "%";
            else itemText[0].text = itemPer.ToString() + "%";
        }
        else if (m_EventSystem.currentSelectedGameObject == slotButtons[1])
        {
            slotPanels[0].SetActive(false);
            slotPanels[1].SetActive(true);
            slotPanels[2].SetActive(false);

            emptyPanels[0].SetActive(false);
            emptyPanels[1].SetActive(false);
            emptyPanels[2].SetActive(false);

            DataManager.instance.LoadData(1);
            areaText[1].text = DataManager.instance.areaName;
            if (DataManager.instance.discoveredRooms < 10) mapText[1].text = "00" + DataManager.instance.discoveredRooms.ToString() + "%";
            else if (DataManager.instance.discoveredRooms >= 10 && DataManager.instance.discoveredRooms < 100) mapText[1].text = "0" + DataManager.instance.discoveredRooms.ToString() + "%";
            else mapText[1].text = DataManager.instance.discoveredRooms.ToString() + "%";
            var timeSpan = System.TimeSpan.FromSeconds(DataManager.instance.time);
            timeText[1].text = timeSpan.Hours.ToString("00") + ":" + timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00");
            bossFlame_2[0].SetActive(DataManager.instance.boss1);
            bossFlame_2[1].SetActive(DataManager.instance.boss2);
            bossFlame_2[2].SetActive(DataManager.instance.boss3);
            //Porcentaje de items
            int itemPer = Mathf.RoundToInt((DataManager.instance.itemsCollected * 100) / DataManager.instance.totalItems);
            if (itemPer < 10) itemText[1].text = "00" + itemPer.ToString() + "%";
            else if (itemPer >= 10 && itemPer < 100) itemText[1].text = "0" + itemPer.ToString() + "%";
            else itemText[1].text = itemPer.ToString() + "%";
        }
        else if (m_EventSystem.currentSelectedGameObject == slotButtons[2])
        {
            slotPanels[0].SetActive(false);
            slotPanels[1].SetActive(false);
            slotPanels[2].SetActive(true);

            emptyPanels[0].SetActive(false);
            emptyPanels[1].SetActive(false);
            emptyPanels[2].SetActive(false);

            DataManager.instance.LoadData(2);
            areaText[2].text = DataManager.instance.areaName;
            if (DataManager.instance.discoveredRooms < 10) mapText[2].text = "00" + DataManager.instance.discoveredRooms.ToString() + "%";
            else if (DataManager.instance.discoveredRooms >= 10 && DataManager.instance.discoveredRooms < 100) mapText[2].text = "0" + DataManager.instance.discoveredRooms.ToString() + "%";
            else mapText[2].text = DataManager.instance.discoveredRooms.ToString() + "%";
            var timeSpan = System.TimeSpan.FromSeconds(DataManager.instance.time);
            timeText[2].text = timeSpan.Hours.ToString("00") + ":" + timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00");
            bossFlame_3[0].SetActive(DataManager.instance.boss1);
            bossFlame_3[1].SetActive(DataManager.instance.boss2);
            bossFlame_3[2].SetActive(DataManager.instance.boss3);
            //Porcentaje de items
            int itemPer = Mathf.RoundToInt((DataManager.instance.itemsCollected * 100) / DataManager.instance.totalItems);
            if (itemPer < 10) itemText[2].text = "00" + itemPer.ToString() + "%";
            else if (itemPer >= 10 && itemPer < 100) itemText[2].text = "0" + itemPer.ToString() + "%";
            else itemText[2].text = itemPer.ToString() + "%";
        }

        if (m_EventSystem.currentSelectedGameObject == emptyButtons[0])
        {
            emptyPanels[0].SetActive(true);
            emptyPanels[1].SetActive(false);
            emptyPanels[2].SetActive(false);

            slotPanels[0].SetActive(false);
            slotPanels[1].SetActive(false);
            slotPanels[2].SetActive(false);

            SetDefaultData();
        }
        else if (m_EventSystem.currentSelectedGameObject == emptyButtons[1])
        {
            emptyPanels[0].SetActive(false);
            emptyPanels[1].SetActive(true);
            emptyPanels[2].SetActive(false);

            slotPanels[0].SetActive(false);
            slotPanels[1].SetActive(false);
            slotPanels[2].SetActive(false);

            SetDefaultData();
        }
        else if (m_EventSystem.currentSelectedGameObject == emptyButtons[2])
        {
            emptyPanels[0].SetActive(false);
            emptyPanels[1].SetActive(false);
            emptyPanels[2].SetActive(true);

            slotPanels[0].SetActive(false);
            slotPanels[1].SetActive(false);
            slotPanels[2].SetActive(false);

            SetDefaultData();
        }
    }

    //Datos default para iniciar una partida nueva
    void SetDefaultData()
    {
        //DataManager.instance.maxHealth = 10;
        DataManager.instance.playerPositionX = -46.747f;
        DataManager.instance.playerPositionY = 0;
        DataManager.instance.areaName = "Train";
        DataManager.instance.time = 0.0f;
        DataManager.instance.discoveredRooms = 0;
        DataManager.instance.inventoryCombat.Clear();
        DataManager.instance.inventoryPassive.Clear();
        DataManager.instance.inventorySpirit.Clear();

        DataManager.instance.blueCombatEssence = "";
        DataManager.instance.blueSpiritEssence = "";
        DataManager.instance.bluePassiveEssence.Clear();
        DataManager.instance.greenCombatEssence = "";
        DataManager.instance.greenSpiritEssence = "";
        DataManager.instance.greenPassiveEssence.Clear();
        DataManager.instance.redCombatEssence = "";
        DataManager.instance.redSpiritEssence = "";
        DataManager.instance.redPassiveEssence.Clear();

        DataManager.instance.boss1 = false;
        DataManager.instance.boss2 = false;
        DataManager.instance.boss3 = false;

        DataManager.instance.blue = false;
        DataManager.instance.green = false;
        DataManager.instance.red = false;

        DataManager.instance.doubleJump = false;
        DataManager.instance.spiritAbility = false;

        DataManager.instance.itemsCollected = 0;
    }

    void CheckFiles()
    {
        //Checar si existen o no datos guardados para poner los botones correspondientes
        for (int i = 0; i < slotButtons.Length; i++)
        {
            slotButtons[i].SetActive(SaveSystem.CheckFileExist(i));
            emptyButtons[i].SetActive(!SaveSystem.CheckFileExist(i));

            shrineOn[i].SetActive(SaveSystem.CheckFileExist(i));
            shrineOff[i].SetActive(!SaveSystem.CheckFileExist(i));
        }

        //Establecer la opcion que estara activa al comienzo
        if(slotButtons[0].activeInHierarchy)
        {
            slotButtons[0].GetComponent<Button>().Select();
        }
        else
        {
            emptyButtons[0].GetComponent<Button>().Select();
        }
    }

    public void CreateNewFile(int _slot)
    {
        DataManager.instance.SaveData(_slot);
        NewGame();
    }

    void NewGame()
    {
        LevelLoader.instance.LoadScene("Game");
    }

    public void LoadGame(int _slot)
    {
        DataManager.instance.LoadData(_slot);
        LevelLoader.instance.LoadScene("Game");
    }

    public void DeleteGame(int _slot)
    {
        SaveSystem.DeleteData(_slot);
        CheckFiles();
    }

    public void GoToSettings()
    {
        LevelLoader.instance.LoadScene("Settings");
    }

    public void GoToCredits()
    {
        LevelLoader.instance.LoadScene("Credits");
    }

    public void QuitApp()
    {
        if (!Application.isEditor) System.Diagnostics.Process.GetCurrentProcess().Kill();
    }
}
