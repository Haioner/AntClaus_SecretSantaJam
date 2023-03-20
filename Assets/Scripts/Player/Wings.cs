using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wings : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float distToGround = 2f;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform body;
    [SerializeField] private Rigidbody rb;

    private void Update()
    {
        var vel = rb.velocity;

        if (IsGrounded())
        {
            anim.speed = vel.magnitude * speed;
        }
        else
        {
            if (vel.magnitude < 2)
                anim.speed = speed;
            else
                anim.speed = vel.magnitude * speed;
        }       
    }

    bool IsGrounded()
    {
        return Physics.Raycast(body.position, -Vector3.up, distToGround);
    }

}
