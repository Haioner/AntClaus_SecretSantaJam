using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSeed : MonoBehaviour
{
    [SerializeField] private float timer = 1f;
    MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        InvokeRepeating("RandomNumber", timer, timer);
    }

    void RandomNumber()
    {
        float randNumber = Random.Range(0.01f, 1f);
        meshRenderer.material.SetFloat("_ColorValue", randNumber);
    }
}
