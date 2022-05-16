using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainTrigger : MonoBehaviour
{
    public GameObject curtain;
    public bool isCloseEntry;
    public bool canCloseEntry;
    public bool thereIsBoss;
    public bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!thereIsBoss && !isOpen)
        {
            OpenEntry();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canCloseEntry && thereIsBoss)
        {
            if (collision.CompareTag("Player"))
            {
                CloseEntry();
            }
        }
    }

    public void CloseEntry()
    {
        isCloseEntry = true;
        curtain.SetActive(true);
    }

    public void OpenEntry()
    {
        isOpen = true;
        isCloseEntry = false;
        curtain.SetActive(false);
    }
}
