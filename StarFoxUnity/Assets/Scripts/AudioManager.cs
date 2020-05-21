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

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        // add reaction to pause menu
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
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
    }

    public void PlaySound()
    {
        int l = audio.Length;
        int index = Random.Range(0, l);
        audioSource.loop = loop;
        audioSource.volume = volume;
        audioSource.clip = audio[index];
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
