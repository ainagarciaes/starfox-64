using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(int soundID, bool loop)
    {
        audio.volume = 1f;
        //audio.clip = collisionSoundEffect;
        audio.Play();
    }

    public void LowerVolume()
    {
        audio.volume = 1f;
    }

    public void ResetVolume()
    {
        audio.volume = 1f;
    }
}
