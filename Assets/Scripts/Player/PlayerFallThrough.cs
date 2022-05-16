using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFallThrough : MonoBehaviour
{
    private PlayerCrouch crouch;
    public bool fallThrough = false;
    private float timer;
    public float timerTime = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        crouch = GetComponent<PlayerCrouch>();
        timer = 0.0f;
    }

    public void Update()
    {
        if(fallThrough)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                fallThrough = false;
            }
        }
    }

    public void FallThroughPlatform(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if (crouch.isCrouching)
            {
                fallThrough = true;
                timer = timerTime;
            }
        }
    }
}
