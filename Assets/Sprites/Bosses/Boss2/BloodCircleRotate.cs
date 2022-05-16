using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCircleRotate : MonoBehaviour
{
    public float rpm;
    public GameObject[] circles = new GameObject[3];

    void Update()
    {
        transform.Rotate(Vector3.forward, rpm * Time.deltaTime);
        circles[0].transform.Rotate(Vector3.back, rpm * Time.deltaTime);
        circles[1].transform.Rotate(Vector3.back, rpm * Time.deltaTime);
        circles[2].transform.Rotate(Vector3.back, rpm * Time.deltaTime);
    }
}
