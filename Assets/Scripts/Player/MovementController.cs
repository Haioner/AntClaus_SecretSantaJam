using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float forwardSpeed = 15f;
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float upSpeed = 5f;
    [SerializeField] private float downSpeed = 5f;
    private float currentSpeed;

    [Header("Rotation")]
    [SerializeField] private float rotSpeed = 7;
    [SerializeField] private Transform cam;

    [Header("Audio")]
    [SerializeField] private AudioSource flyAudio;
    [SerializeField] private float minVolume;
    [SerializeField] private Vector2 minMaxPitch;

    private Rigidbody rb;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RotateToCameraView();
        SpeedInputs();
        PlayFlyAudio();
    }

    private void FixedUpdate()
    {
        FlyToCameraView();
    }

    private void RotateToCameraView()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, cam.transform.rotation, rotSpeed * Time.deltaTime);
    }

    private void FlyToCameraView()
    {
        Vector3 offset = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))).normalized * currentSpeed * Time.fixedDeltaTime;
        rb.AddForce(offset, ForceMode.VelocityChange);

        //Force up and down
        if (Input.GetButton("Jump"))
            rb.AddForce(transform.up * upSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        else if (Input.GetKey(KeyCode.LeftControl))
            rb.AddForce(-transform.up * downSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }

    private void PlayFlyAudio()
    {
        float interpolatedVolume = Mathf.Lerp(minVolume, .75f, rb.velocity.magnitude / 7f);
        flyAudio.volume = interpolatedVolume;
        float interpolatedPitch = Mathf.Lerp(minMaxPitch.x, minMaxPitch.y, rb.velocity.magnitude / 7f);
        flyAudio.pitch = interpolatedPitch;
    }

    private void SpeedInputs()
    {
        if (Input.GetAxisRaw("Vertical") > 0.01f && !Input.GetButton("Jump") && !Input.GetKey(KeyCode.LeftControl))
            currentSpeed = forwardSpeed;
        else
            currentSpeed = baseSpeed;
    }
}
