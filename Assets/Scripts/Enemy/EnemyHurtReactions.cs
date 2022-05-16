using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class EnemyHurtReactions : MonoBehaviour
{
    public MMFeedback hurtAnimation;
    public MMFeedback dieParticles;
    private EnemyBase enemy;

    private void Awake()
    {
        enemy = gameObject.GetComponentInParent<EnemyBase>();
        if (!enemy) {
            enemy = gameObject.transform.parent.GetComponentInChildren<EnemyBase>();
        }
        dieParticles.enabled = false;
    }

    public void getHurt() {
        if (enemy.currentHealth <= 0)
        {
            Invoke("Die", enemy.tiempoDeMuerte - 0.3f);
        }
        else {
            hurtAnimation?.Play(transform.position);
        }
    }

    public void Die() {
        dieParticles.enabled = true;
        dieParticles?.Initialization(this.gameObject);
        dieParticles?.Play(transform.position);
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
