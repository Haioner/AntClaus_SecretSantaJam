using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 axisMultiplier;

    private void Update()
    {
        transform.position = new Vector3
            (playerTransform.position.x + axisMultiplier.x,
            playerTransform.position.y + axisMultiplier.y,
            playerTransform.position.z + axisMultiplier.z);
    }
}
