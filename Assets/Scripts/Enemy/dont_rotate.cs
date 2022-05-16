using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dont_rotate : MonoBehaviour
{
    private Quaternion rot;
    public bool ignorar_x = false;

    private void Awake()
    {
        rot = transform.rotation;
    }

    private void LateUpdate()
    {
        if (!ignorar_x)
        {
            transform.rotation = rot;
        }
        else {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, rot.eulerAngles.y, transform.rotation.eulerAngles.z));
        }
       
    }

}
