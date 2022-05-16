using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TofukozoEssence : CombatEssence
{
    public float speedX;
    public float speedY;

    public override void Activate(GameObject parent)
    {
        Debug.Log("Tofu used!");

        
        if (parent.transform.rotation.y < 0)
        {
            GameObject spawnedObject = Instantiate(objectToInstantiate, parent.transform.position + new Vector3(-0.5f * parent.transform.localScale.x, 0), parent.transform.rotation);
            spawnedObject.GetComponent<Rigidbody2D>().velocity = new Vector2((speedX * -1) * parent.transform.localScale.x, speedY * parent.transform.localScale.y);
        }
        else
        {
            GameObject spawnedObject = Instantiate(objectToInstantiate, parent.transform.position + new Vector3(0.5f * parent.transform.localScale.x, 0), parent.transform.rotation);
            spawnedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speedX * parent.transform.localScale.x, speedY * parent.transform.localScale.y);
        }
    }
}
