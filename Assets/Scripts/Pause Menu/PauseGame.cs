using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

public class PauseGame : MonoBehaviour
{
    //Animator del jugador
    Animator anim;
    Animation currentAnim;

    //Canvas usados en el menu de pausa
    [SerializeField]
    GameObject mapPanel;
    [SerializeField]
    GameObject pauseCanvas;
    [SerializeField]
    GameObject settingsCanvas;
    [SerializeField]
    GameObject aurasCanvas;
    [SerializeField]
    GameObject movesetCanvas;

    //Sonidos Abrir y Cerrar menu pausa
    FMOD.Studio.EventInstance openMenu;
    FMOD.Studio.EventInstance closeMenu;

    bool paused = false;

    private void Start()
    {
        openMenu = FMODUnity.RuntimeManager.CreateInstance("event:/UI/openMenu");
        closeMenu = FMODUnity.RuntimeManager.CreateInstance("event:/UI/closeMenu");
        anim = GameManager.Instance.player.GetComponent<Animator>();
    }



    public void OpenMap(InputAction.CallbackContext context)
    {
        if (context.performed && GameManager.Instance.canPause)
        {
            if (mapPanel.activeInHierarchy)
            {
                mapPanel.SetActive(false);
                PauseTime();
            }
            else if(pauseCanvas.activeInHierarchy || settingsCanvas.activeInHierarchy || aurasCanvas.activeInHierarchy || movesetCanvas.activeInHierarchy)
            {
                ClosePauseMenu();
                mapPanel.SetActive(true);
            }
            else
            {
                mapPanel.SetActive(true);
                PauseTime();
            }
            
        }
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if(context.performed && GameManager.Instance.canPause)
        {
            if(pauseCanvas.activeInHierarchy)
            {
                ClosePauseMenu();
                PauseTime();
            }
            else if(settingsCanvas.activeInHierarchy || aurasCanvas.activeInHierarchy || movesetCanvas.activeInHierarchy)
            {
                ClosePauseMenu();
                PauseTime();
            }
            else if(mapPanel.activeInHierarchy)
            {
                mapPanel.SetActive(false);
                pauseCanvas.SetActive(true);
            }
            else
            {
                PlayOpenMenuEvent();
                pauseCanvas.SetActive(true);
                PauseTime();
            }
        }
    }

    public void ClosePauseMenu()
    {
        PlayCloseMenuEvent();
        pauseCanvas.SetActive(false);
        mapPanel.SetActive(false);
        settingsCanvas.SetActive(false);
        aurasCanvas.SetActive(false);
        movesetCanvas.SetActive(false);
    }

    public void PauseTime()
    {
        paused = !paused;
        if(paused)
        {
            GameManager.Instance.pause = true;
            GameManager.Instance.stopPlayer = true;
            //anim.GetCurrentAnimatorStateInfo(0).sets = 0.0f;
            Time.timeScale = 0;
            //anim.speed = 0;
        }
        else
        {
            GameManager.Instance.pause = false;
            GameManager.Instance.stopPlayer = false;
            //currentAnim[anim.GetCurrentAnimatorClipInfo(0)[0].clip.name].speed = 1.0f;
            //GameManager.Instance.player.GetComponent<PlayerMovement>().horizontal = Vector2.zero;
            //anim.speed = 1;
            Time.timeScale = 1;
            //Si estas en la animacion de modo espiritu regresa a la misma animacion al quitar la pausa
            if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "player_spirit_idle")
            {
                anim.Rebind();
                anim.SetBool("spirited_out", false);
                anim.Play("player_spirit_idle");
            }
            else //Si estas en otra animacion regresa al idle al quitar la pausa
            {
                anim.Rebind();//Reiniciar animator
            }
        }
    }

    public void PlayCloseMenuEvent()
    {
        closeMenu = FMODUnity.RuntimeManager.CreateInstance("event:/UI/closeMenu");
        closeMenu.start();
        closeMenu.release();
    }

    public void PlayOpenMenuEvent()
    {
        openMenu = FMODUnity.RuntimeManager.CreateInstance("event:/UI/openMenu");
        openMenu.start();
        openMenu.release();
    }
}
