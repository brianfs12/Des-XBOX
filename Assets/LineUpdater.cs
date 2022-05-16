using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineUpdater : MonoBehaviour
{
    public LineRenderer line;
    public Transform pos1;
    public Transform pos2;
    public float LineWidth; // use the same as you set in the line renderer.

    // Start is called before the first frame update
    void Start()
    {
        LineWidth = line.startWidth;
        line.positionCount = 2;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        line.SetPosition(0, pos1.position);
        line.SetPosition(1, pos2.position);
    }     
}
