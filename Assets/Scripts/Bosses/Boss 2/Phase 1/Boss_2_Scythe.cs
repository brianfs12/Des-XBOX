using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Scythe : MonoBehaviour
{
    public float rpm = 600f;
    public float movementSpeed = 10f;
    public GameObject[] points = new GameObject[4];
    public int index;
    public float waitToStart;
    bool started;
    bool stop;


    void Start()
    {
        index = 0;
        started = true;
        stop = false;
    }

    public void Die()
    {
        LeanTween.pause(gameObject);
        GetComponent<CircleCollider2D>().enabled = false;
        LeanTween.moveLocal(gameObject, new Vector3(-12.0f, -1.281f, 0.0f), 1.0f).setOnComplete(Rotate);
    }

    void Rotate()
    {
        stop = true;
        Destroy(gameObject);
    }

    void Update()
    {
        waitToStart -= Time.deltaTime;

        if (waitToStart <= 0.0f && !stop)
        {
            if(started)
            {
                started = false;
                LeanTween.move(gameObject, points[0].transform.position, movementSpeed).setOnComplete(MoveToNextPoint).setOnCompleteParam(index++);
            }
            //Girar
            transform.Rotate(Vector3.back, rpm * Time.deltaTime);
            if(transform.rotation.z >= 0 && transform.rotation.z <.05)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/enemies/bosses/boss2/weaponSpin", transform.position);
            }
        }
    }

    public void MoveToNextPoint()
    {
        if (index == 4)
        {
            index = 0;
        }
        LeanTween.move(gameObject, points[index].transform.position, movementSpeed).setOnComplete(MoveToNextPoint).setOnCompleteParam(index++);
    }
}
