using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class timeToMenu : MonoBehaviour
{
    private float countdownTime = 20.0f;

    // Update is called once per frame
    void Update()
    {
        countdownTime -= Time.deltaTime;
        if (countdownTime <= 0.0f)
        {
            SceneManager.LoadScene(0);
        }
    }
}
