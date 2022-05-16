using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public float life;
    public MMFeedbacks particlesWallCollision;

    void Start()
    {
        if (GameManager.Instance.playerShoot.isPointingUp)
        {
            rb.velocity = transform.up * speed;
            gameObject.transform.Rotate(0f, 0f, 90);
        }
        else if(GameManager.Instance.playerShoot.isPointingDown)
        {
            rb.velocity = -transform.up * speed;
            gameObject.transform.Rotate(0f, 0f, -90);
        }
        else
        {
            rb.velocity = transform.right * speed;
        }

        Destroy(gameObject, life);
    }

    private void Update()
    {
        StartCoroutine("BulletFade");
    }

    IEnumerator BulletFade()
    {
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

        }
        else if(collision.CompareTag("Enemy") && collision.gameObject.name != "Scythe" && collision.gameObject.name != "HitobarashiSpikes")
        {
            BulletCollision();
            collision.GetComponent<EnemyBase>().TakeDamage(GameManager.Instance.playerStats.currentATK);
            if (transform.parent == null && collision.GetComponentInChildren<EnemyHurtReactions>())
            {
                collision.GetComponentInChildren<EnemyHurtReactions>().getHurt();
            }
            else {
                collision.transform.parent.gameObject.GetComponentInChildren<EnemyHurtReactions>().getHurt();
            }
        }
        else if (collision.CompareTag("Boss"))
        {
            BulletCollision();
            collision.transform.parent.GetComponent<Boss1Base>().TakeDamage(GameManager.Instance.playerStats.currentATK);
        }
        else if(collision.gameObject.CompareTag("Boss2"))
        {
            BulletCollision();
            collision.transform.GetComponent<Boss_2_Base>().TakeDamage(GameManager.Instance.playerStats.currentATK);
        }
        else if (collision.gameObject.CompareTag("Boss3"))
        {
            BulletCollision();
        }
        else if (collision.CompareTag("floor") || collision.CompareTag("slope") || collision.gameObject.name == "HitobarashiSpikes")
        {
            particlesWallCollision?.Initialization(this.gameObject);
            particlesWallCollision?.PlayFeedbacks(transform.position);
            particlesWallCollision.transform.parent = null; 
            BulletCollision();
        }
    }
    public void BulletCollision()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/playerSounds/shotImpact", transform.position);
        Destroy(gameObject);
    }
}
