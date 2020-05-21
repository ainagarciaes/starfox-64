using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSound : MonoBehaviour
{
    public bool oneShot = false;
    // Start is called before the first frame update
    void Start()
    {
        if (oneShot) 
            this.gameObject.GetComponent<AudioManager>().PlaySingleSound(0);
        else
            this.gameObject.GetComponent<AudioManager>().PlaySound();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
