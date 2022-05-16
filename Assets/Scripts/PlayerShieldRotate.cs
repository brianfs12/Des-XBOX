using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerShieldRotate : MonoBehaviour
{
    private ParentConstraint constraints; 
    private void Start()
    {
        constraints = GetComponent<ParentConstraint>();
        //Transform trans = GameObjec
        //constraints.SetSource(1, );
    }
    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(0, 0, -180 * Time.deltaTime);
    }
}
