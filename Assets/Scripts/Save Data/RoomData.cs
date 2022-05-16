using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData : MonoBehaviour
{
    public enum ZonaColar { BlueZone, GreenZone, RedZone, SafeZone };
    public ZonaColar zoneColor;
    [SerializeField]
    public int id;
    [SerializeField]
    public bool visited;
    FMOD.Studio.EventInstance music;

    public void Entrar()
    {
        visited = true;
        CheckZone();
        GameManager.Instance.gameProgress.roomsUI[id].SetActive(true);
        GameManager.Instance.gameProgress.roomsUI[id].transform.GetChild(0).gameObject.SetActive(true);
        GameManager.Instance.gameProgress.CheckDiscoveredRooms();
    }

    public void CheckZone()
    {
        if(zoneColor == ZonaColar.BlueZone)
        {
            if (!GameManager.Instance.isOnBlueZone)
            {
                DecideSounds(true, false, false, false, "event:/Music/musicZone1");
            }
        }
        else if (zoneColor == ZonaColar.GreenZone)
        {
            if (!GameManager.Instance.isOnGreenZone)
            {
                DecideSounds(false, true, false, false, "event:/Music/musicZone2");
            }
        }
        else if (zoneColor == ZonaColar.RedZone)
        {
            if (!GameManager.Instance.isOnRedZone)
            {
                DecideSounds(false, false, true, false, "event:/Music/musicZone3");
            }
        }
        else if (zoneColor == ZonaColar.SafeZone)
        {
            if (!GameManager.Instance.isOnSafeZone)
            {
                DecideSounds(false, false, false, true, "event:/Music/safeRoom");
            }
        }
    }

    public void Salir()
    {
        GameManager.Instance.gameProgress.roomsUI[id].transform.GetChild(0).gameObject.SetActive(false);
    }

    public void DecideSounds(bool blue, bool green, bool red, bool safe, string path)
    {
        GameManager.Instance.isOnBlueZone = blue;
        GameManager.Instance.isOnGreenZone = green;
        GameManager.Instance.isOnRedZone = red;
        GameManager.Instance.isOnSafeZone = safe;
        music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        PlayMusicSound(path);
    }

    public void PlayMusicSound(string path)
    {
        //music = null;
        music = FMODUnity.RuntimeManager.CreateInstance(path);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(music, transform);
        music.start();
        music.release();
    }
}
