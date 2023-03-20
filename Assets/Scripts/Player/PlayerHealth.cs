using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float MaxVelocityToDamage = 6f;
    [SerializeField] private Vector3 velocityRB;

    [Header("Health")]
    [SerializeField] private int currentHealth = 3;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Checker")]
    [SerializeField] private LayerMask layer;
    [SerializeField] private float distanceRightLeft = 1f;
    [SerializeField] private float distanceFrontBack = 2f;
    [SerializeField] private Vector3 rightLeftPos;
    [SerializeField] private Vector3 frontBackPos;

    [Header("SpeedLineParticle")]
    [SerializeField] private ParticleSystem speedLine;

    [Header("Hit")]
    [SerializeField] private AudioManager hitAudio;
    [SerializeField] private AudioManager splatAudio;
    [SerializeField] private GameObject hitParticle;

    private Rigidbody rb;
    private bool isColliding = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        healthText.text = currentHealth.ToString();
    }

    private void Update()
    {
        if (!isColliding)
            velocityRB = rb.velocity;

        SpeedLines();
    }

    private void OnCollisionEnter(Collision collision)
    {
        isColliding = true;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.right, out hit, distanceRightLeft, layer))
        {
            if (velocityRB.x >= MaxVelocityToDamage)
                Damage();
        }
        else if (Physics.Raycast(transform.position, -Vector3.right, out hit, distanceRightLeft, layer))
        {
            if (velocityRB.x <= -MaxVelocityToDamage)
                Damage();
        }

        Collider[] frontColl = Physics.OverlapSphere(transform.position + frontBackPos, distanceFrontBack, layer);
        Collider[] backColl = Physics.OverlapSphere(transform.position - frontBackPos, distanceFrontBack, layer);
        if (frontColl.Length > 0 && velocityRB.z >= MaxVelocityToDamage)
            Damage();

        if (backColl.Length > 0 &&velocityRB.z <= -MaxVelocityToDamage)
            Damage();

        velocityRB = new Vector3(0, 0, 0);
    }

    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.right * distanceRightLeft);
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, -Vector3.right * distanceRightLeft);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + frontBackPos, distanceFrontBack);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position - frontBackPos, distanceFrontBack);
    }

    private void SpeedLines()
    {
        var speedRate = speedLine.emission;
        if (rb.velocity.magnitude >= MaxVelocityToDamage)
            speedRate.rateOverTime = 25;
        else
            speedRate.rateOverTime = 0;
    }

    private void Damage()
    {
        CameraShake.instance.ShakeCamera(5f, 0.3f);
        hitAudio.PlayAudio(1);
        Instantiate(hitParticle, transform);
        currentHealth--;
        healthText.text = currentHealth.ToString();
        CheckHealth();
    }

    private void CheckHealth()
    {
        if(currentHealth <= 0)
        {
            hitAudio.StopAudio();
            splatAudio.PlayAudio(1);
            FindObjectOfType<Transition>().PlayOutTransition("Game");
        }
    }
}
