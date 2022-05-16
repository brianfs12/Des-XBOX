using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3DartThrow : MonoBehaviour
{
    public GameObject player;
    private Vector3 relativePos;
    public float offset_rotación = 0.02f; //entre más grande, más tarda en rotar hacia el jugador
    public float proyectileSpeed;
    public GameObject proyectile;

    void Update()
    {
        //transform.LookAt(player.transform,transform.up);
        relativePos = player.transform.position - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(relativePos);
        LeanTween.rotate(this.gameObject, lookRot.eulerAngles, offset_rotación);
    }

    public void disparar() {
        GameObject spawnedObject = Instantiate(proyectile, transform.position + new Vector3(0.5f * transform.localScale.x, 0), transform.rotation);
        spawnedObject.transform.LookAt(player.transform);
        spawnedObject.GetComponent<Rigidbody2D>().velocity = transform.forward * proyectileSpeed;
    }
}
