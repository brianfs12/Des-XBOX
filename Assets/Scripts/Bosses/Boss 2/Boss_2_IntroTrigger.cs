using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Boss_2_IntroTrigger : MonoBehaviour
{
    public Animator background;
    public GameObject boss;
    public GameObject weapon;
    public Animator bossUIAnim;
    public MetalCurtains metalCurtains;
    public GameObject cam2;
    public GameObject cam3;
    public CinemachineVirtualCamera cam1VC;
    public CinemachineVirtualCamera cam2VC;
    public CinemachineBrain cinemachineBrain;
    public GameObject bgDefeated;

    private void Start()
    {
        //Destruirse si ya fue derrotado
        if(DataManager.instance)
        {
            if (DataManager.instance.boss2)
            {
                bgDefeated.SetActive(true);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.StopPlayer();
            GameManager.Instance.canPause = false;
            metalCurtains.closeCurtain();
            cinemachineBrain.m_DefaultBlend.m_Style = Cinemachine.CinemachineBlendDefinition.Style.EaseIn;
            cinemachineBrain.m_DefaultBlend.m_Time = 0.5f;
            cam2.SetActive(true);
            background.SetTrigger("Intro");
            gameObject.SetActive(false);
        }
    }

    public void ChangeToCamera3()
    {
        cam3.SetActive(true);
    }

    public void ChangeToCamera2()
    {
        cam3.SetActive(false);
        cam2.SetActive(true);
        cam2VC.Follow = cam1VC.Follow;
    }

    public void ActiveBoss()
    {
        boss.SetActive(true);
        weapon.SetActive(true);
        bossUIAnim.gameObject.SetActive(true);
        bossUIAnim.SetBool("intro", false);
        bossUIAnim.SetBool("die", false);
        GameManager.Instance.ResumePlayer();
        GameManager.Instance.canPause = true;
    }
}
