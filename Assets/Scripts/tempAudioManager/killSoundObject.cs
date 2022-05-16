using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killSoundObject : MonoBehaviour
{
    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(!source.isPlaying)
        {
            print(name + " stopped playing");
            Destroy(gameObject);
        }
    }
}
