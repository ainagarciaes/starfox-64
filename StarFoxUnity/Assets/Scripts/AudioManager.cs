using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audio;
    AudioSource audioSource;
    public bool loop;
    public bool pause_lower;
    bool paused = false;
    public float volume;
    public bool spatialSound = false;
    public bool random = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // add reaction to pause menu
        paused = LevelManager.Instance.IsGamePaused();

        if (paused && pause_lower)
        {
            LowerVolume();
        }

        else if (!paused && pause_lower)
        {
            ResetVolume();
        }
        else if (paused && !pause_lower)
        {
            audioSource.Pause();
        }
        else if (!paused && !pause_lower)
        {
            audioSource.UnPause();
        }

    }

    public void PlaySound()
    {
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        int l = audio.Length;
        int index = Random.Range(0, l);
        audioSource.clip = audio[index];
        audioSource.loop = loop;
        if (random) 
            audioSource.time = Random.Range(0, audioSource.clip.length / 2);
        audioSource.volume = volume;

        if (spatialSound)
        {
            audioSource.spatialBlend = 1;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
        }
        audioSource.Play();
    }

    public void PlaySingleSound()
    {
        int l = audio.Length;
        int index = Random.Range(0, l);
        audioSource.volume = volume;
        audioSource.PlayOneShot(audio[index]);
    }
    public void PlaySingleSound(int index)
    {
        PlaySingleSound(index, volume);
    }

    public void PlaySingleSound(int index, float customVolume)
    {
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
