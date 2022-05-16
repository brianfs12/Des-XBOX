using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    //Cadencia de disparo
    public float shootCadence;
    float timer = 0.0f;
    bool canShoot = true;

    public Transform shootPoint;
    public GameObject projectilePrefab;
    public Vector2 normalShootPoint;
    public Vector2 upShootPoint;
    public Vector2 downShootPoint;
    public bool isPointingUp;
    public bool isPointingDown;
    public GameObject aimSprite;
    public bool isJumpig;
    public bool isOnPlace;
    public bool downInput = false;

    private PlayerSpiritMode playerSpiritMode;


    void Start()
    {
        normalShootPoint = new Vector2(0.25f, -0.5f);
        upShootPoint = new Vector2(0f, .5f);
        downShootPoint = new Vector2(0f, -1.5f);
        isPointingUp = false;
        isPointingDown = false;
        playerSpiritMode = GetComponent<PlayerSpiritMode>();
        isJumpig = false;
        isOnPlace = false;
    }

    void Update()
    {
        CheckInputs();
        if (!isJumpig && !isOnPlace && !isPointingUp)
        {
            ShootPointOnPlace();
        }

        if(!canShoot)
        {
            timer += Time.deltaTime;
            if((timer % 60) >= shootCadence)
            {
                canShoot = true;
            }
        }
    }

    public void CheckInputs()
    {
        if(downInput && (isJumpig || playerSpiritMode.anim.GetBool("flying")))
        {
            isOnPlace = false;
            isPointingDown = true;
            shootPoint.transform.localPosition = new Vector3(downShootPoint.x, downShootPoint.y, 0);
            aimSprite.transform.localPosition = new Vector3(0f, -1.5f, 0.0f);
        }
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed && !playerSpiritMode.spirited && !GameManager.Instance.stopPlayer && canShoot)//Get Key Down
        {
            canShoot = false;
            timer = 0.0f;
            FMODUnity.RuntimeManager.PlayOneShot("event:/playerSounds/shoot", shootPoint.position);
            Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        }
    }
    public void PointUp(InputAction.CallbackContext context) //AimUp?
    {
        if (context.performed && !playerSpiritMode.spirited)//Get Key Down
        {
            isPointingUp = true;
            isOnPlace = false;
            shootPoint.transform.localPosition = new Vector3(upShootPoint.x, upShootPoint.y, 0);
            aimSprite.transform.localPosition = new Vector3(0.0f, 0.5f, 0.0f);

        }
        if (context.canceled && !playerSpiritMode.spirited)
        {
            ShootPointOnPlace();
        }
    }
    public void PointDown(InputAction.CallbackContext context)
    {
        if (context.performed && !playerSpiritMode.spirited)//Get Key Down
        {
            downInput = true;
        }
        if(context.canceled && !playerSpiritMode.spirited)
        {
            downInput = false;
            ShootPointOnPlace();
        }
    }

    void ShootPointOnPlace()
    {
        isOnPlace = true;
        isPointingUp = false;
        isPointingDown = false;
        shootPoint.transform.localPosition = new Vector3(normalShootPoint.x, normalShootPoint.y, 0);
        aimSprite.transform.localPosition = new Vector3(0.6f, 0.0f, 0.0f);
    }
}
