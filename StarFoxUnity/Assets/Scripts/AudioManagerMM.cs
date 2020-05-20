using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerMM : MonoBehaviour
{
    public AudioClip[] audio;
    AudioSource audioSource;
    public bool loop;
    public bool pause_lower;
    bool paused = false;
    public float volume;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        int l = audio.Length;
        int index = Random.Range(0, l);
        audioSource.loop = loop;
        audioSource.volume = volume;
        audioSource.clip = audio[index];
        audioSource.Play();
    }
    public void PlaySingleSound(int index)
    {
        PlaySingleSound(index, volume);
    }

    public void PlaySingleSound(int index, float customVolume)
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = customVolume;
        if (index == 1) audioSource.volume = 1;
        audioSource.PlayOneShot(audio[index]);
    }

    public void StopSound()
    {
        audioSource.Stop();
    }

    public void LowerVolume()
    {
        audioSource.volume = 0.5f * volume;
    }

    public void ResetVolume()
    {
        audioSource.volume = volume;
    }

    public void RiseVolume()
    {
        audioSource.volume = 4 * volume;
    }
}
