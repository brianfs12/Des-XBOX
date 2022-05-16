using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss3 : MonoBehaviour
{
    public GameObject player;
    public GameObject sprite;
    public bool dasheando;

    public enum Direccion { izq, der };

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerStats>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dasheando) {
            if (player.transform.position.x > transform.position.x)
            {
                sprite.transform.rotation = Quaternion.Euler(0, 90, 0);
                print("derecha");
            }
            else
            {
                sprite.transform.rotation = Quaternion.Euler(0, -90, 0);
                print("izquierda");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(1, this.gameObject);
        }
    }

    public void SetDashear(bool d) {
        dasheando = d;
    }
}
