using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    public Camera cam;
    public GameObject player;

    private bool playerOnUI;
    // Update is called once per frame
    void Update()
    {
        Vector3 viewPos = cam.WorldToViewportPoint(player.transform.position);
        if (viewPos.x < 0.2f && viewPos.y > 0.8f)
        {
            if(!playerOnUI)
            {
                controlAlphaDown();
                playerOnUI = true;
            }
            
        }
        else
        {
            if (playerOnUI)
            {
                controlAlphaUp();
                playerOnUI = false;
            }
        }
    }

    void controlAlphaDown()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform Children = this.transform.GetChild(i);
            Image img = Children.GetComponent<Image>();
            
            if (img != null)
            {
                img.color = new Color(1, 1, 1, 0.1F);
            }
            else
            {
                for (int j = 0; j < Children.childCount; j++)
                {
                    Transform Children2 = Children.transform.GetChild(j);
                    Image img2 = Children2.GetComponent<Image>();
                    if (img2 != null)
                    {
                        img2.color = new Color(1, 1, 1, 0.1F);
                    }
                }
            }

            SlicedFilledImage sliceImg = Children.GetComponent<SlicedFilledImage>();
            if (sliceImg != null)
            {
                sliceImg.color = new Color(1, 1, 1, 0.1F);
            }
            else
            {
                for (int j = 0; j < Children.childCount; j++)
                {
                    Transform Children2 = Children.transform.GetChild(j);
                    SlicedFilledImage sliceImg2 = Children2.GetComponent<SlicedFilledImage>();
                    if (sliceImg2 != null)
                    {
                        sliceImg2.color = new Color(1, 1, 1, 0.1F);
                    }
                }
            }
        }
    }

    void controlAlphaUp()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform Children = this.transform.GetChild(i);
            Image img = Children.GetComponent<Image>();
            if (img != null)
            {
                img.color = new Color(1, 1, 1, 1);
            }
            else
            {
                for (int j = 0; j < Children.childCount; j++)
                {
                    Transform Children2 = Children.transform.GetChild(j);
                    Image img2 = Children2.GetComponent<Image>();
                    if (img2 != null)
                    {
                        img2.color = new Color(1, 1, 1, 1);
                    }
                }
            }

            SlicedFilledImage sliceImg = Children.GetComponent<SlicedFilledImage>();
            if (sliceImg != null)
            {
                sliceImg.color = new Color(1, 1, 1, 1);
            }
            else
            {
                for (int j = 0; j < Children.childCount; j++)
                {
                    Transform Children2 = Children.transform.GetChild(j);
                    SlicedFilledImage sliceImg2 = Children2.GetComponent<SlicedFilledImage>();
                    if (sliceImg2 != null)
                    {
                        sliceImg2.color = new Color(1, 1, 1, 1);
                    }
                }
            }
        }
    }
}
