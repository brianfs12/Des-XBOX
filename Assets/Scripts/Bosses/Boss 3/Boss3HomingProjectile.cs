using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class Boss3HomingProjectile : MonoBehaviour
{
    public GameObject player;
    public PlayMakerFSM fsm;
    private Vector3 relativePos;
    public float speed = 5;

    // Start is called before the first frame update
    void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        relativePos = player.transform.position - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(relativePos);
        this.gameObject.transform.rotation = lookRot;
        FsmVector2 goal_pos = fsm.FsmVariables.GetFsmVector2("player_direction");
        goal_pos.Value = new Vector2(transform.forward.x, transform.forward.y).normalized * speed;
        FsmGameObject goal_object = fsm.FsmVariables.GetFsmGameObject("player");
        goal_object.Value = player;
    }
}
