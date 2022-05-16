using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1trigger : MonoBehaviour
{
    public Animator playerAnim;
    public Animator propsAnim;
    public Animator canvasAnim;
    public MetalCurtains metalCurtains;

    private void Start()
    {
        //Destruirse si ya fue derrotado
        if(DataManager.instance)
        {
            if (DataManager.instance.boss1)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            metalCurtains.closeCurtain();
            canvasAnim.gameObject.SetActive(true);
            playerAnim.SetBool("intro", false);
            propsAnim.SetBool("intro", false);
            canvasAnim.SetBool("intro", false);
            canvasAnim.SetBool("die", false);
            gameObject.SetActive(false);
        }
    }

    public void die() {
        playerAnim.SetBool("die", true);
        propsAnim.SetBool("die", true);
        canvasAnim.SetBool("die", true);

        canvasAnim.SetBool("intro", true);
    }
}
