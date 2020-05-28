using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCart : MonoBehaviour
{
    public bool enabled = false;
    [SerializeField] GameObject LookAtObj;
    [SerializeField] GameObject PlayerArea;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float extraSpeed = (LookAtObj.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed - 20) / 4; 
        if (!enabled) return;
        transform.position = PlayerArea.transform.position +(PlayerArea.transform.position - LookAtObj.transform.position).normalized*(50+extraSpeed);
        transform.LookAt(LookAtObj.transform.position);
    }
}
