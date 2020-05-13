using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audio;
    AudioSource audioSource;
    public bool loop;
    public bool paused = false;
    public float volume;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // add reaction to pause menu
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            if (paused && loop)
            {
                LowerVolume();
            }

            else if (!paused && loop)
            {
                ResetVolume();
            }
            else if (paused && !loop)
            {
                audioSource.Pause();
            }
            else if (!paused && !loop)
            {
                audioSource.UnPause();
            }
        }
    }

    public void PlaySound()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        int l = audio.Length;
        int index = Random.Range(0, l);
        audioSource.loop = loop;
        audioSource.volume = 1f;
        audioSource.clip = audio[index];
        audioSource.Play();
    }

    public void LowerVolume()
    {
        audioSource.volume = 0.5f*volume;
    }

    public void ResetVolume()
    {
        audioSource.volume = volume;
    }
}
