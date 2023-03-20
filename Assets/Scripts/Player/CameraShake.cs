using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance { get; private set; }
    public CinemachineFreeLook cinemachine;
    private float shakeTimer;

    private void Awake()
    {
        instance = this;
        cinemachine = GetComponent<CinemachineFreeLook>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        for (int i = 0; i < 2; i++)
        {
            CinemachineVirtualCamera cameras = cinemachine.GetRig(i);
            cameras.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        }

        shakeTimer = time;
    }

    private void Update()
    {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0f)
            {
                for (int i = 0; i < 2; i++)
                {
                    CinemachineVirtualCamera cameras = cinemachine.GetRig(i);
                    cameras.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
                }
            }
        }
    }
}
