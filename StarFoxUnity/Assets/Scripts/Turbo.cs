using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbo : MonoBehaviour
{
    const float regularSpeed = 20, minSpeed = 5, maxSpeed = 70;
    [SerializeField] Cinemachine.CinemachineDollyCart cs;

    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            if (cs.m_Speed < maxSpeed) cs.m_Speed += cs.m_Speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (cs.m_Speed > minSpeed) cs.m_Speed -= cs.m_Speed * Time.deltaTime;
        }
        else if (cs.m_Speed < regularSpeed) cs.m_Speed += cs.m_Speed * Time.deltaTime;
        else cs.m_Speed -= cs.m_Speed * Time.deltaTime;
    }
}
