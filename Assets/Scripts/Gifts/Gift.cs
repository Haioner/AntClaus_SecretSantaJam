using UnityEngine;

public class Gift : MonoBehaviour
{
    [SerializeField] private Material[] giftMaterials;
    [SerializeField] private AudioManager giftAudio;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        GetComponent<MeshRenderer>().material = giftMaterials[Random.Range(0, giftMaterials.Length)];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<Gift_Manager>().AddGift(1);
            anim.Play("Taken");
            giftAudio.PlayAudio(1);
        }
    }

    public void DestroyEvent()
    {
        Destroy(gameObject);
    }
}
