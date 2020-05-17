using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    GameObject audioTitle;

    private void Start()
    {
        audioTitle = GameObject.Find("AudioClicks");
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitGame()
    {
        audioTitle.GetComponent<AudioManager>().PlaySound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void InitLevel2()
    {
        audioTitle.GetComponent<AudioManager>().PlaySound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void CreditsScene()
    {
        audioTitle.GetComponent<AudioManager>().PlaySound();
        Cursor.visible = false;
        SceneManager.LoadScene(3);
    }

    public void QuitGame()
    {
        audioTitle.GetComponent<AudioManager>().PlaySound();
        Application.Quit();
    }
}
