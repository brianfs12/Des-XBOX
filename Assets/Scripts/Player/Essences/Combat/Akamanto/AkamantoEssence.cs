using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AkamantoEssence : CombatEssence
{
    public float projectileSpeed;
    public GameObject altObject;
    private bool red;

    public override void Activate(GameObject parent)
    {
        GameObject spawnedObject;
        if (!red)
        {
            if(parent.transform.rotation.y < 0)
            {
                spawnedObject = Instantiate(objectToInstantiate, parent.transform.position + new Vector3(-0.5f * parent.transform.localScale.x, -0.5f), parent.transform.rotation);
                spawnedObject.GetComponent<Rigidbody2D>().velocity = new Vector2((projectileSpeed * -1) * parent.transform.localScale.x, 0);
            }
            else
            {
                spawnedObject = Instantiate(objectToInstantiate, parent.transform.position + new Vector3(0.5f * parent.transform.localScale.x, -0.5f), parent.transform.rotation);
                spawnedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed * parent.transform.localScale.x, 0);
            }
            red = true;
        }
        else
        {
            if(parent.transform.rotation.y < 0)
            {
                spawnedObject = Instantiate(altObject, parent.transform.position + new Vector3(-0.5f * parent.transform.localScale.x, -0.5f), parent.transform.rotation);
                spawnedObject.GetComponent<Rigidbody2D>().velocity = new Vector2((projectileSpeed * -1) * parent.transform.localScale.x, 0);
            }
            else
            {
                spawnedObject = Instantiate(altObject, parent.transform.position + new Vector3(0.5f * parent.transform.localScale.x, -0.5f), parent.transform.rotation);
                spawnedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed * parent.transform.localScale.x, 0);
            }
            red = false;
        }
    }
}
