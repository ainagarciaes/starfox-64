using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbo : MonoBehaviour
{
    const float regularSpeed = 20, minSpeed = 5, maxSpeed = 70;
    [SerializeField] Cinemachine.CinemachineDollyCart cs;


    const int maxTurbo = 10;
    float currentTurbo;
    bool turboAvailable;

    private void Start()
    {
        turboAvailable = true;
        currentTurbo = maxTurbo;
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.W) && turboAvailable)
        {
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
                if (cs.m_Speed > minSpeed) cs.m_Speed -= cs.m_Speed * Time.deltaTime;
            }
            else if (cs.m_Speed < regularSpeed) cs.m_Speed += cs.m_Speed * Time.deltaTime;
            else cs.m_Speed -= cs.m_Speed * Time.deltaTime;
        }
        print(currentTurbo);
    }
}
