using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ScreenResolutions
{
    public int width { get; set; }
    public int height { get; set; }
}

public class SettingsScreenResolution : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public List<ScreenResolutions> resolutions = new List<ScreenResolutions>();
    ScreenResolutions screenResolutions;
    int widthBase = 320;
    int heightBase = 180;

    bool canChange = true;

    public TextMeshProUGUI resolutionText;

    public Button resolutionBtn;

    GameObject whiteSquare;

    public void OnSelect(BaseEventData eventData)
    {
        whiteSquare.SetActive(true);
    }

    public void OnDeselect(BaseEventData data)
    {
        whiteSquare.SetActive(false);
    }

    EventSystem m_EventSystem;

    void OnEnable()
    {
        m_EventSystem = EventSystem.current;
    }

    void Start()
    {
        whiteSquare = this.transform.GetChild(0).gameObject;

        for (int i = 1; i <= 4; i++)
        {
            screenResolutions = new ScreenResolutions();
            screenResolutions.height = heightBase * i;
            screenResolutions.width = widthBase * i;
            resolutions.Add(screenResolutions);
        }

        screenResolutions = new ScreenResolutions();
        screenResolutions.height = 1080;
        screenResolutions.width = 1920;
        resolutions.Add(screenResolutions);

        ChangeResolutionText(DataManager.instance.currentResolutionIndex);
    }

    private void Update()
    {
        if(m_EventSystem.currentSelectedGameObject == resolutionBtn.gameObject)
        {
            if(Input.GetAxisRaw("Horizontal") == 1 && DataManager.instance.currentResolutionIndex < 4 && canChange)
            {
                canChange = false;
                DataManager.instance.currentResolutionIndex++;
            }
            if (Input.GetAxisRaw("Horizontal") == -1 && DataManager.instance.currentResolutionIndex > 0 && canChange)
            {
                canChange = false;
                DataManager.instance.currentResolutionIndex--;
            }
            if(Input.GetAxisRaw("Horizontal") != 1 && Input.GetAxisRaw("Horizontal") != -1)
            {
                canChange = true;
            }
        }
        ChangeResolutionText(DataManager.instance.currentResolutionIndex);
    }

    public void ChangeResolution()
    {
        DataManager.instance.currentResolutionWidth = resolutions[DataManager.instance.currentResolutionIndex].width;
        DataManager.instance.currentResolutionHeight = resolutions[DataManager.instance.currentResolutionIndex].height;
        Screen.SetResolution(resolutions[DataManager.instance.currentResolutionIndex].width, resolutions[DataManager.instance.currentResolutionIndex].height, resolutions[DataManager.instance.currentResolutionIndex].width == 1920);
    }

    public void ChangeResolutionText(int _currentIndex)
    {
        resolutionText.text = resolutions[_currentIndex].width.ToString() + "x" + resolutions[_currentIndex].height.ToString();
    }
}
