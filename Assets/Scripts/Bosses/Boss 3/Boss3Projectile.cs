using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class Boss3Projectile : MonoBehaviour
{
    public GameObject player;
    public PlayMakerFSM fsm;
    private Vector3 relativePos;
    public float speed = 5;

    public enum TipoBala { normal, bulletRain};
    public TipoBala tipo = TipoBala.normal;

    public GameObject[] posiciones;

    // Start is called before the first frame update
    void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;

        if (tipo == TipoBala.bulletRain) {
            posiciones = GameObject.FindGameObjectsWithTag("HomingSpawn");
            transform.position = posiciones[Random.Range(0, posiciones.Length-1)].transform.position;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 20, transform.localPosition.z);
        }

        relativePos = player.transform.position - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(relativePos);
        this.gameObject.transform.rotation = lookRot;
        FsmVector2 goal_pos = fsm.FsmVariables.GetFsmVector2("player_direction");
        goal_pos.Value = new Vector2(transform.forward.x, transform.forward.y).normalized * speed;
        FsmGameObject goal_object = fsm.FsmVariables.GetFsmGameObject("player");
        goal_object.Value = player;
    }

    private void Update()
    {
        if (transform.localPosition.y < -20) {
            Destroy(gameObject);
        }
    }
}
