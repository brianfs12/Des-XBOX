using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShaker : MonoBehaviour
{
    public static CameraShaker instance { get; private set; }
    CinemachineBrain brain;
    CinemachineVirtualCamera vCam;

    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    [SerializeField] private float intensityLargo;
    [SerializeField] private float timeLargo;
    [SerializeField] private float intensityCorto;
    [SerializeField] private float timeCorto;

    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
        brain = GetComponent<CinemachineBrain>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Shake(40,1);
        }*/

        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 - shakeTimer / shakeTimerTotal);
        }
    }
    
    public void ShakeLargo()
    {
        vCam = brain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
        //print(vCam);
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensityLargo;

        startingIntensity = intensityLargo;
        shakeTimerTotal = timeLargo;
        shakeTimer = timeLargo;
    }

    public void ShakeCorto()
    {
        vCam = brain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
        //print(vCam);
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensityCorto;

        startingIntensity = intensityCorto;
        shakeTimerTotal = timeCorto;
        shakeTimer = timeCorto;
    }
}
