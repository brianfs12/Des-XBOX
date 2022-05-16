using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Camera : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player.transform.position.y > 5.0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 6.45f, transform.position.z), 0.1f);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 0.0f, transform.position.z), 0.1f);
        }
    }
}
