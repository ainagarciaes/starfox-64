using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<AudioManager>().PlaySound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
