using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PaseMenuController : MonoBehaviour
{
    Transform menu;
    Transform instr;

    void Start()
    {
        menu = this.gameObject.transform.GetChild(0);
        instr = this.gameObject.transform.GetChild(1);
        ToPauseMenu();
    }

    public void ToInstructionsMenu()
    {
        menu.gameObject.SetActive(false);
        instr.gameObject.SetActive(true);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        transform.gameObject.SetActive(false);
    }

    public void ToPauseMenu()
    {
        menu.gameObject.SetActive(true);
        instr.gameObject.SetActive(false);
    }
}
