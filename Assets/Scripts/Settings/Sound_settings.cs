using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sound_settings : MonoBehaviour
{
    [SerializeField]
    Slider BgmSlider;
    [SerializeField]
    Slider SfxSlider;
    [SerializeField]
    GameObject settingsPanel;
    [SerializeField]
    GameObject rebindPanel;
    public GameObject settingsHeader;
    public Button SFXBtn;

    void Start()
    {
        //Cargar los settings guardados
        if (SaveSystem.CheckSettingsFileExist())
        {
            DataManager.instance.LoadSettings();
        }
        else
        {
            SetDefaultSettings();
        }

    }

    public void SetDefaultSettings()
    {
        DataManager.instance.BgmVolume = 1.0f;
        DataManager.instance.SfxVolume = 1.0f;
        DataManager.instance.currentResolutionIndex = 4;
        DataManager.instance.currentResolutionHeight = 1080;
        DataManager.instance.currentResolutionWidth = 1920;
        AssingnSettingsValues();
    }

    void AssingnSettingsValues() 
    {
        BgmSlider.value = DataManager.instance.BgmVolume;
        SfxSlider.value = DataManager.instance.SfxVolume;
    }

    public void SetBgmVolume()
    {
        DataManager.instance.BgmVolume = BgmSlider.value;
    }
    public void SetSfxVolume()
    {
        DataManager.instance.SfxVolume = SfxSlider.value;
    }

    public void Rebind()
    {
        settingsPanel.SetActive(false);
        rebindPanel.SetActive(true);
        settingsHeader.SetActive(false);
    }

    public void BackRebind()
    {
        settingsPanel.SetActive(true);
        settingsHeader.SetActive(true);
        rebindPanel.SetActive(false);
        SFXBtn.Select();
    }

    public void ReturnToMain() 
    {
        SaveSettings();
        if (SceneManager.GetActiveScene().name == "Settings")
        {
            LevelLoader.instance.LoadScene("mainMenu");
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveSettings()
    {
        DataManager.instance.SaveSettings();
    }
}
