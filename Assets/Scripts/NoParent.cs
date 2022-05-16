using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        while (transform.parent.transform.parent) {
            transform.parent = transform.parent.transform.parent;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
