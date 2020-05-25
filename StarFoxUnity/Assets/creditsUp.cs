using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditsUp : MonoBehaviour
{
    float speed;
    public GameObject audio;
    // Start is called before the first frame update
    void Start()
    {
        speed = (Screen.height+250)/20f;
        audio.GetComponent<AudioManagerMM>().PlaySound();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
