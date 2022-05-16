using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class TofuSpin : MonoBehaviour
{
    Timeline timeline;
    Rigidbody2D rb;

    private void Start()
    {
        timeline = GetComponent<Timeline>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if(timeline != null)
        {
            transform.Rotate(0, 0, -50 * Time.deltaTime * timeline.timeScale);
        }
        else
        {
            transform.Rotate(0, 0, -50 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("floor") && rb.velocity.y < -0.1f) //Para que solo se destruyan si chocan contra el suelo
        {
            Destroy(gameObject);
        }
    }
}
