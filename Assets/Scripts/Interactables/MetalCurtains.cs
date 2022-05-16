using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalCurtains : MonoBehaviour
{
    public enum CurtainType {TallLight1, TallLight2, TallDark1, TallDark2, TallBoss};
    public CurtainType curtainType;

    public BoxCollider2D closedCollider;
    public BoxCollider2D openedCollider;

    public SpriteRenderer spriteRenderer;

    [Header("Open Sprites")]
    public Sprite[] openSpriteList;

    [Header("Closed Sprites")]
    public Sprite[] closeSpriteList;

    public bool open;

    private Animator anim;

    FMOD.Studio.EventInstance openSound;
    FMOD.Studio.EventInstance closeSound;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (open)
        {
            collisionOpen();
            spriteRenderer.sprite = openSpriteList[(int)curtainType];
        }
        else
        {
            collisionClose();
            spriteRenderer.sprite = closeSpriteList[(int)curtainType];
        }
    }

    private void Update()
    {
        
        /*if(Input.GetKeyDown(KeyCode.K))
        {
            if(open)
            {
                closeCurtain();
            }
            else
            {
                openCurtain();
            }
        }*/
        
    }
    //Se llama cada que se actualiza una variable desde el inspector
    private void OnValidate()
    {
        if (open)
        {
            spriteRenderer.sprite = openSpriteList[(int)curtainType];
        }
        else
        {
            spriteRenderer.sprite = closeSpriteList[(int)curtainType];
        }
    }

    public void openCurtain()
    {
        anim.Play(curtainType.ToString() + "Open");
    }

    public void closeCurtain()
    {
        anim.Play(curtainType.ToString() + "Close");
    }

    public void collisionOpen()
    {
        openedCollider.enabled = true;
        closedCollider.enabled = false;
        //spriteRenderer.sprite = openSpriteList[(int)curtainType];
        open = true;
    }

    public void collisionClose()
    {
        open = false;
        openedCollider.enabled = false;
        closedCollider.enabled = true;
        //spriteRenderer.sprite = closeSpriteList[(int)curtainType];
    }
    public void PlayOpenEvent()
    {
        openSound = FMODUnity.RuntimeManager.CreateInstance("event:/Misc/curtainOpen");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(openSound, transform);
        openSound.start();
        openSound.release();
    }
    public void PlayCloseEvent()
    {
        closeSound = FMODUnity.RuntimeManager.CreateInstance("event:/Misc/curtainClose");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(closeSound, transform);
        closeSound.start();
        closeSound.release();
    }
}
