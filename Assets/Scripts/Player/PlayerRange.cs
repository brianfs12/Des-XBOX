using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRange : MonoBehaviour
{
    public CircleCollider2D triggerRange;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        /*
        if (collision.CompareTag("PlayerProjectile"))
        {
            Destroy(collision.gameObject);
        }
        */
    }
    public void UpdateRadius()
    {
        triggerRange.radius = GameManager.Instance.playerStats.bulletRange;
    }
}
