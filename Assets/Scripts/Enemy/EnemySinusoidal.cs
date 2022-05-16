//Azael Sanchez
//Enemigo que sigue al jugador y se mueve en curva senoidal
//Editado por Alan Penafiel para reproducir audio
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class EnemySinusoidal : EnemyBase
{
    public int maxHealth;
    public float moveSpeed;
    public bool activeMovement;

    Timeline timeline;

    [Header("Misc")]
    [SerializeField]
    public float frequency;
    public float magnitude;

    GameObject player;

    private FMOD.Studio.EventInstance audioLoop;
    private FMOD.Studio.PLAYBACK_STATE pbState;

    void Start()
    {
        audioLoop = FMODUnity.RuntimeManager.CreateInstance("event:/enemies/mobs/skeleFloat");
        currentHealth = maxHealth;
        player = GameObject.Find("Playeer");
        timeline = GetComponent<Timeline>();
    }

    private void OnDisable()
    {
        activeMovement = false;
        audioLoop.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    void FixedUpdate()
    {
        audioLoop.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform, GetComponent<Rigidbody2D>()));
        audioLoop.getPlaybackState(out pbState);
        if (Vector2.Distance(player.transform.position, gameObject.transform.position) <= 8f)
        {
            activeMovement = true;
            if(pbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                audioLoop.start();
        }
        if (activeMovement)
        {
            Movement();
        }
    }

    void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime * timeline.timeScale);
        transform.position += transform.up * Mathf.Sin(Time.time * timeline.timeScale * frequency) * magnitude;
    }
}
