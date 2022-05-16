using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticles : MonoBehaviour
{
    ParticleSystem water;
    ParticleSystem waves;
    ParticleSystem dropsFront;

    float timerWater;
    float timerWaves;

    public float waterRate;
    public float wavesRate;

    Transform player;
    Rigidbody2D playerRB;
    public PlayerMovement playerMovement;

    void Start()
    {
        water = transform.GetChild(0).GetComponent<ParticleSystem>();
        waves = transform.GetChild(1).GetComponent<ParticleSystem>();
        dropsFront = transform.GetChild(2).GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(playerMovement == null)
            playerMovement = collision.GetComponent<PlayerMovement>();
            print(playerMovement);
            Rigidbody2D playerRBTemp = collision.gameObject.GetComponent<Rigidbody2D>();

            player = collision.transform;
            if(playerRBTemp != null)
            playerRB = collision.gameObject.GetComponent<Rigidbody2D>();
            water.gameObject.transform.position = new Vector3(player.position.x, water.gameObject.transform.position.y, 0.0f);
            waves.gameObject.transform.position = new Vector3(player.position.x, water.gameObject.transform.position.y, 0.0f);
            water.Play();
            waves.Play();
            playerMovement.isWalkingOnWater = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (playerRB != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (playerRB.velocity.x > -0.1f && playerRB.velocity.x < 0.1f)
                {
                    dropsFront.Stop();
                }

                if (playerRB.velocity.x < -0.1f || playerRB.velocity.x > 0.1f)
                {
                    timerWaves += Time.deltaTime;
                    timerWater += Time.deltaTime;

                    water.gameObject.transform.position = new Vector3(player.position.x, water.gameObject.transform.position.y, 0.0f);
                    waves.gameObject.transform.position = new Vector3(player.position.x, water.gameObject.transform.position.y, 0.0f);
                    dropsFront.gameObject.transform.position = new Vector3(player.position.x, water.gameObject.transform.position.y, 0.0f);
                    if (timerWater >= waterRate)
                    {
                        if (playerRB.velocity.x < -0.1f)
                        {
                            dropsFront.gameObject.transform.localEulerAngles = new Vector3(196.0f, 90.0f, 0.0f);
                        }
                        else if (playerRB.velocity.x > 0.1f)
                        {
                            dropsFront.gameObject.transform.localEulerAngles = new Vector3(-16.0f, 90.0f, 0.0f);
                        }
                        water.Play();
                        dropsFront.Play();
                        timerWater = 0.0f;
                    }
                    if (timerWaves >= wavesRate)
                    {
                        waves.Play();
                        timerWaves = 0.0f;
                    }
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(playerMovement == null)
            playerMovement = collision.GetComponent<PlayerMovement>();
            playerMovement.isWalkingOnWater = false;
        }
    }
}
