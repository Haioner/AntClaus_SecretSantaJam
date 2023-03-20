using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private float initialVolume = 1f;
    [SerializeField] private float speedVolume = 1f;
    [SerializeField] private bool destroyOnStop = false;
    private AudioSource audioSource;
    private bool audioState = false;
    private bool calledStop = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        AudioVolumeChange();
    }

    private void AudioVolumeChange()
    {
        if (audioState)
        {
            if (audioSource.volume < initialVolume)
                audioSource.volume += speedVolume * Time.deltaTime;
        }
        else
        {
            if (audioSource.volume > 0)
                audioSource.volume -= speedVolume * Time.deltaTime;
            else if (destroyOnStop && calledStop)
                Destroy(gameObject);
        }
    }

    public void PlayAudio(float volume)
    {
        if (!audioSource.isPlaying)
            audioSource.Play();

        initialVolume = volume;
        audioState = true;
    }

    public void StopAudio()
    {
        calledStop = true;
        audioState = false;
    }
}
