using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using PlayMaker;

public class boss3trigger : MonoBehaviour
{
    public Animator canvasAnim;
    public GameObject bossInvisibleWalls;
    public GameObject VcamBoss;
    public CinemachineBrain cinemachineBrain;
    public PlayMakerFSM fsm;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            ShowHPBar();
            GameManager.Instance.StopPlayer();
            bossInvisibleWalls.SetActive(true);
            cinemachineBrain.m_DefaultBlend.m_Style = Cinemachine.CinemachineBlendDefinition.Style.EaseIn;
            cinemachineBrain.m_DefaultBlend.m_Time = 1.5f;
            VcamBoss.SetActive(true);
            fsm.SendEvent("END");
            gameObject.SetActive(false);
        }
    }

    public void ShowHPBar()
    {
        canvasAnim.SetBool("die", false);
        canvasAnim.SetBool("intro", false);
    }

    public void HideHPBar()
    {
        canvasAnim.SetBool("die", true);
        canvasAnim.SetBool("intro", true);
    }

    public void die() {
        canvasAnim.SetBool("die", true);
        canvasAnim.SetBool("intro", true);
    }
}
