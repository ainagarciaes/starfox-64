using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbo : MonoBehaviour
{

    const float minSpeed = 5, maxSpeed = 70;

    float regularSpeed, regularVolume;
    [SerializeField] AudioManager thrustersAudio;
    [SerializeField] Cinemachine.CinemachineDollyCart cs;

    const int maxTurbo = 10;
    float currentTurbo;
    bool turboAvailable;
    bool audioVol;
    private void Start()
    {
        audioVol = thrustersAudio != null;
        regularSpeed = cs.m_Speed;
        if (audioVol)
            regularVolume = thrustersAudio.volume;
        turboAvailable = true;
        currentTurbo = maxTurbo;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) && turboAvailable)
        {
            if (audioVol) thrustersAudio.SetVolume(regularVolume * 4);
            if (cs.m_Speed < maxSpeed) cs.m_Speed += cs.m_Speed * Time.deltaTime;
            currentTurbo -= Time.deltaTime * 2;
            turboAvailable = (currentTurbo > 0);
            if (!turboAvailable) currentTurbo = 0;
        }
        else
        {

            if (currentTurbo < maxTurbo) currentTurbo += Time.deltaTime;
            if (!turboAvailable)
            {
                turboAvailable = !(currentTurbo < maxTurbo);
                if (turboAvailable) currentTurbo = maxTurbo;
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (audioVol) thrustersAudio.SetVolume(regularVolume *2f/ 3f);
                if (cs.m_Speed > minSpeed) cs.m_Speed -= cs.m_Speed * Time.deltaTime;
            }
            else
            {
                if (audioVol) thrustersAudio.ResetVolume(); ;
                if (cs.m_Speed < regularSpeed) cs.m_Speed += cs.m_Speed * Time.deltaTime;
                else cs.m_Speed -= cs.m_Speed * Time.deltaTime;
            }
        }
        //print(turboAvailable + " " + currentTurbo);
    }

    public float GetTurboValue()
    {
        if (turboAvailable) return currentTurbo / maxTurbo;
        return -currentTurbo / maxTurbo;
    }
}
