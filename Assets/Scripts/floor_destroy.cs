using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floor_destroy : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("floor"))
        {
            Destroy(this.gameObject);
        }
    }

}
