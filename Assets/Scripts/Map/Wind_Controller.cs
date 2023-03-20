using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind_Controller : MonoBehaviour
{
    [Header("Cache")]
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private ParticleSystem windParticle;
    [SerializeField] private ParticleSystem snowParticle;
    [SerializeField] private AudioManager windAudio;

    [Header("Force")]
    [SerializeField] private float currentWindForce;
    [SerializeField] private Vector2 minMaxForce;
    [SerializeField] private Vector3 direction;

    [Header("Timer")]
    [SerializeField] private float currentTimerToWind = 10f;
    [SerializeField] private float currentTimerToStopWind = 4f;
    private float initialTimerToWind;
    private float initialTimerToStopWind;

    private void Start()
    {
        initialTimerToWind = currentTimerToWind;
        initialTimerToStopWind = currentTimerToStopWind;
    }

    private void Update()
    {
        TimerToWind();
        ParticleVelocity();
        ParticleRate();
    }

    private void FixedUpdate()
    {
        ApplyForce(playerRB);
    }

    #region Custom Methods

    private void TimerToWind()
    {
        if (currentTimerToWind > 0)
            currentTimerToWind -= 1f * Time.deltaTime;
        else
        {
            currentTimerToWind = initialTimerToWind;
            currentTimerToStopWind = initialTimerToStopWind;
            RandomizeWind();
        }

        if (currentTimerToStopWind > 0)
            currentTimerToStopWind -= 1f * Time.deltaTime;
        else
        {
            StopWind();
        }
    }

    private void ApplyForce(Rigidbody rb)
    {
        rb.AddForce(direction * currentWindForce * Time.deltaTime, ForceMode.Impulse);
    }

    private void RandomizeWind()
    {
        float randomForce = Random.Range(minMaxForce.x, minMaxForce.y);
        currentWindForce = randomForce;

        int randomX = Random.Range(-1, 2);
        int randomZ = Random.Range(-1, 2);

        //If Z == 0 and X takes 0, X will try get 1/-1
        if (randomZ == 0)
        {
            while (randomX == 0)
            {
                randomX = Random.Range(-1, 2);
            }
        }

        //If X == 0 and Z takes 0, Z will try get 1/-1
        if (randomX == 0)
        {
            while(randomZ == 0)
            {
                randomZ = Random.Range(-1, 2);
            }
        }

        Vector3 randomDirection = new Vector3(randomX, 0, randomZ);
        direction = randomDirection;

        windAudio.PlayAudio((currentWindForce/minMaxForce.y) * 1.5f);
    }

    private void StopWind()
    {
        currentWindForce = 0;
        direction = new Vector3(0, 0, 0);
        windAudio.StopAudio();
    }

    private void ParticleVelocity()
    {
        //Wind particle
        var vel = windParticle.velocityOverLifetime;
        vel.x = direction.x * currentWindForce;
        vel.y = direction.y * currentWindForce;
        vel.z = direction.z * currentWindForce;

        //Snow particle
        var snowVel = snowParticle.velocityOverLifetime;
        snowVel.x = direction.x;
        snowVel.z = direction.z;
    }

    private void ParticleRate()
    {
        var rate = windParticle.emission;
        rate.rateOverTime = currentWindForce * 4;
    }

    #endregion
}
