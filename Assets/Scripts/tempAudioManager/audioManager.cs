using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public static audioManager instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject soundObject;
    public GameObject currentMusicObject;

    //efectos de sonido
    public AudioClip sfx_test1, sfx_test2;

    //musica
    public AudioClip bgMusic_test1, bgMusic_test2;

    public void PlaySfx(string _soundName)
    {
        switch(_soundName)
        {
            case "test1":
                CreateSoundObject(sfx_test1);
                break;
            case "test2":
                CreateSoundObject(sfx_test2);
                break;
        }
    }

    void CreateSoundObject(AudioClip _clip)
    {
        GameObject spawnedObject = Instantiate(soundObject, transform);

        spawnedObject.GetComponent<AudioSource>().clip = _clip;

        spawnedObject.GetComponent<AudioSource>().Play();
    }

    public void PlayMusic(string _soundName)
    {
        switch (_soundName)
        {
            case "music":
                CreateMusicObject(bgMusic_test1);
                break;
            case "music2":
                CreateMusicObject(bgMusic_test2);
                break;
        }
    }

    void CreateMusicObject(AudioClip _clip)
    {
        if(currentMusicObject)
        {
            Destroy(currentMusicObject);
        }

        currentMusicObject = Instantiate(soundObject, transform);

        currentMusicObject.GetComponent<AudioSource>().clip = _clip;

        currentMusicObject.GetComponent<AudioSource>().loop = true;

        currentMusicObject.GetComponent<AudioSource>().Play();
    }
}
