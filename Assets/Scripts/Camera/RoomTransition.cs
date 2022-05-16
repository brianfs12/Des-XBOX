using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomTransition : MonoBehaviour
{
    public GameObject vCam;
    public GameObject goPool;
    public Animator Transitionanim;

    RoomData data;


    private PlayerCrouch player;

    private void Start()
    {
        data = GetComponentInParent<RoomData>();
        //Intente automatizar el pedo de la lista de GameObjects pero no jalo lmao
        /*
        Transform t = transform.Find("GameObjectsList");
        if(t != null)
        {
            goPool = t.gameObject;
        } 
        */
        if(goPool != null)
        {
            goPool.SetActive(false);
        }
        player = FindObjectOfType<PlayerCrouch>();
        vCam.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Transition") && !collision.isTrigger && !vCam.activeSelf)
        {
            if (!player.isCrouching) 
            {
                data.Entrar();
                //Transitionanim.SetTrigger("Transition");
                //Transitionanim.Play("FadeIn", 0, 0.0f);
                if (goPool != null)
                {
                    goPool.SetActive(true);
                    for(int i = 0; i < goPool.transform.childCount; i++)
                    {
                        goPool.transform.GetChild(i).gameObject.SetActive(true);
                    }
                }
                vCam.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Transition") && !collision.isTrigger)
        {
            if (goPool != null)
            {
                goPool.SetActive(false);
            }
            if (!player.isCrouching) {
                data.Salir();
                vCam.SetActive(false);
            }
        }
    }
}
