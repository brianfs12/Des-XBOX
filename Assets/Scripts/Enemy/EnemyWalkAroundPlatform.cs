using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkAroundPlatform : MonoBehaviour
{
    public Transform transformChild;
    public float movementSpeed;

    private Rigidbody2D rbody;
    private int directionEnemy = 4;
    FMOD.Studio.EventInstance wheelSound;

    public bool imOnPlatform;
    public Vector2 velocity;

    private void Awake()
    {
        imOnPlatform = true;
        rbody = GetComponent<Rigidbody2D>();
        velocity = new Vector2(movementSpeed, 0);
    }
    /*private void OnEnable()
    {
        rbody.velocity = new Vector2(movementSpeed, 0);
        if (directionEnemy == 1)
        {
            rbody.velocity = new Vector2(0, -movementSpeed);
        }
        else if (directionEnemy == 2)
        {
            rbody.velocity = new Vector2(-movementSpeed, 0);
        }
        else if (directionEnemy == 3)
        {
            rbody.velocity = new Vector2(0, movementSpeed);
        }
        else if (directionEnemy == 4)
        {
            rbody.velocity = new Vector2(movementSpeed, 0);
        }
    }*/
    private void FixedUpdate()
    {
        StartCoroutine("Movement");
    }

    IEnumerator Movement()
    {
        if (!imOnPlatform)
        {
            yield return new WaitForSeconds(.1f);
            velocity = new Vector2(0, 0);
        }
        rbody.velocity = velocity;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("floor") || collision.CompareTag("OneWay"))//el tag de plataforma atravesable es OneWay
        {
            transform.Rotate(0, 0, -90);
            transformChild.Rotate(0,0,90);//Esto es para que no rote el sprite
            if (directionEnemy == 1)
            {
                velocity = new Vector2(0, -movementSpeed);
                directionEnemy = 2;
            }
            else if (directionEnemy == 2)
            {
                velocity = new Vector2(-movementSpeed, 0);
                directionEnemy = 3;
            }
            else if (directionEnemy == 3)
            {
                velocity = new Vector2(0, movementSpeed);
                directionEnemy = 4;
            }
            else if (directionEnemy == 4)
            {
                velocity = new Vector2(movementSpeed, 0);
                directionEnemy = 1;
            }
            //PlayWheelEvent();
            imOnPlatform = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("floor") || collision.CompareTag("OneWay"))//el tag de plataforma atravesable es OneWay
        {

            imOnPlatform = true;
        }
    }

    public void PlayWheelEvent()
    {
        //wheelSound = FMODUnity.RuntimeManager.CreateInstance("");
        wheelSound.start();
        wheelSound.release();
    }
}
