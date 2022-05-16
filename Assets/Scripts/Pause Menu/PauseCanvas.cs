using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using FMODUnity;

public class PauseCanvas : MonoBehaviour
{
    [Header("Buttons")]
    public Button firstSelection;
    public Button buttonToReturnTo;
    public Button settingsButton;
    public Button auraButton;
    public Button movesetButton;

    [Header("Canvas To Open")]
    public GameObject map;
    public GameObject settings;
    public GameObject auras;
    public GameObject moveset;

    [Header("Pause Menu Text")]
    public TextMeshProUGUI description;
    public TextMeshProUGUI playtime;
    public TextMeshProUGUI mapPer;
    public TextMeshProUGUI itemsPer;
    public GameObject boss1Flames;
    public GameObject boss2Flames;
    public GameObject boss3Flames;

    FMOD.Studio.EventInstance movementUI;


    private void OnEnable()
    {
        var timeSpan = System.TimeSpan.FromSeconds(GameManager.Instance.time);
        playtime.text = timeSpan.Hours.ToString("00") + ":" + timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00");
        if (GameManager.Instance.gameProgress.discoveredRooms < 10) mapPer.text = "00" + GameManager.Instance.gameProgress.discoveredRooms.ToString() + "%";
        else if (GameManager.Instance.gameProgress.discoveredRooms >= 10 && GameManager.Instance.gameProgress.discoveredRooms < 100) mapPer.text = "0" + GameManager.Instance.gameProgress.discoveredRooms.ToString() + "%";
        else mapPer.text = GameManager.Instance.gameProgress.discoveredRooms.ToString() + "%";
        //Porcentaje de items
        int itemPer = Mathf.RoundToInt((DataManager.instance.itemsCollected * 100) / DataManager.instance.totalItems);
        if (itemPer < 10) itemsPer.text = "00" + itemPer.ToString() + "%";
        else if (itemPer >= 10 && itemPer < 100) itemsPer.text = "0" + itemPer.ToString() + "%";
        else itemsPer.text = itemPer.ToString() + "%";
        boss1Flames.SetActive(GameManager.Instance.boss1);
        boss2Flames.SetActive(GameManager.Instance.boss2);
        boss3Flames.SetActive(GameManager.Instance.boss3);
    }

    private void OnDisable()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ChangeDescriptionText(string _text)
    {
        description.text = _text;
    }

    public void BackToPause()
    {
        gameObject.SetActive(true);
        map.SetActive(false);
        settings.SetActive(false);
        auras.SetActive(false);
        moveset.SetActive(false);

        EventSystem.current.SetSelectedGameObject(buttonToReturnTo.gameObject);
        //firstSelection.Select();
    }

    public void OpenAuras()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/accept", transform.position);
        buttonToReturnTo = auraButton;
        gameObject.SetActive(false);
        auras.SetActive(true);
    }

    public void OpenMoveset()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/accept", transform.position);
        buttonToReturnTo = movesetButton;
        gameObject.SetActive(false);
        moveset.SetActive(true);
    }

    public void OpenSettings()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/accept", transform.position);
        buttonToReturnTo = settingsButton;
        gameObject.SetActive(false);
        settings.SetActive(true);
    }

    public void OpenMap()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/accept", transform.position);
        buttonToReturnTo = firstSelection;
        gameObject.SetActive(false);
        map.SetActive(true);
    }

    public void Quit()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/accept", transform.position);
        Time.timeScale = 1;
        LevelLoader.instance.LoadScene("mainMenu");
    }

    public void PlayMovementUIEvent()
    {
        movementUI = FMODUnity.RuntimeManager.CreateInstance("event:/UI/moveSelection");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(movementUI, transform);
        movementUI.start();
        movementUI.release();
    }
}
