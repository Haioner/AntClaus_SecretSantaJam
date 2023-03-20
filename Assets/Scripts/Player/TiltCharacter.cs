using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltCharacter : MonoBehaviour
{
    [SerializeField] private float horizontalTilt = 30f;
    [SerializeField] private float verticalTilt = 15f;
    [SerializeField] private float smoothTilt = 2f;

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * horizontalTilt;
        float vertical = Input.GetAxis("Vertical") * verticalTilt;
        var target = Quaternion.Euler(vertical, transform.eulerAngles.y, -horizontal);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smoothTilt);

        Quaternion rot = transform.localRotation;
        rot.y = 0;
        transform.localRotation = rot;
    }
}
