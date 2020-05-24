using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PaseMenuController : MonoBehaviour
{
    Transform menu;
    Transform instr;
    GameObject audioPause;
    void Start()
    {
        menu = this.gameObject.transform.GetChild(1);
        instr = this.gameObject.transform.GetChild(2);
        menu.gameObject.SetActive(true);
        instr.gameObject.SetActive(false);
        audioPause = GameObject.Find("AudioPause");
        ToPauseMenu();
    }

    public void ToInstructionsMenu()
    {
        audioPause.GetComponent<AudioManager>().PlaySound();
        menu.gameObject.SetActive(false);
        instr.gameObject.SetActive(true);
    }

    public void ToMainMenu()
    {
        audioPause.GetComponent<AudioManager>().PlaySound();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        audioPause.GetComponent<AudioManager>().PlaySound();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResumeGame()
    {
        audioPause.GetComponent<AudioManager>().PlaySound();
        Time.timeScale = 1;
        transform.gameObject.SetActive(false);
    }

    public void ToPauseMenu()
    {
        audioPause.GetComponent<AudioManager>().PlaySound();
        menu.gameObject.SetActive(true);
        instr.gameObject.SetActive(false);
    }
}
