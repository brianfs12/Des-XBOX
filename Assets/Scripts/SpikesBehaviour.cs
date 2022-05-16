using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesBehaviour : MonoBehaviour
{
    public Animator EnemyAnimator;
    public EnemyPatrolAreaAttack EnemyGO;
    FMOD.Studio.EventInstance rise;
    FMOD.Studio.EventInstance retract;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Patrol()
    {
        if (EnemyGO.actualSpot == 0)
        {
            EnemyGO.transform.Rotate(0f, 180f, 0f);
            EnemyGO.actualSpot = 1;
        }
        else
        {
            EnemyGO.transform.Rotate(0f, 180f, 0f);
            EnemyGO.actualSpot = 0;
        }
        EnemyGO.imOnPlace = false;
        EnemyGO.attacked = false;
    }
    public void PlayRiseEvent()
    {
        rise = FMODUnity.RuntimeManager.CreateInstance("event:/enemies/mobs/hitobashiraWeaponsRise");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(rise, transform);
        rise.start();
        rise.release();
    }
    public void PlayRetractEvent()
    {
        retract = FMODUnity.RuntimeManager.CreateInstance("event:/enemies/mobs/hitobashiraWeaponsRetract");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(retract, transform);
        rise.start();
        rise.release();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EnemyGO.MakeDamage(damage);
        }
    }
}
