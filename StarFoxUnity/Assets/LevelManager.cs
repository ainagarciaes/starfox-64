using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject GameOverMenu;
    [SerializeField] GameObject GameWinMenu;
    [SerializeField] GameObject GameGUI;
    [SerializeField] GameObject DamageGUI;

    private int hitpoints = 100; // per posar algo, idk es pot adaptar després
    private int score = 0;
    private bool roll = false;

    // Start is called before the first frame update
    void Start()
    { 
        PauseMenu.SetActive(false);
        GameOverMenu.SetActive(false);
        GameWinMenu.SetActive(false);
        GameGUI.SetActive(true);
        DamageGUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // show pause menu
        if(Input.GetKeyDown(KeyCode.P))
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        // rotation code
        else if(!roll && Input.GetKeyDown(KeyCode.R))
        {
            roll = true;
            // todo passar aqui el codi del roll i afegir-hi el que calgui per settejar el roll a false quan acabi
        }
    }

    public void UpdateHitPoints(int damage, int type) // 0: damage per projectils, 1: colisio...
    {
        if (!(roll && type == 0)) // roll evita damage per colisio
        {
            hitpoints -= damage;
            if (hitpoints <= 0)
            {
                GameOver();
            }
            else
            {
                // show damage animation
            }
        }
    }

    public void UpdateScore(int scoreIncr)
    {
        score += scoreIncr;
    }

    public int GetHitPoints()
    {
        return hitpoints;
    }

    public int GetScore()
    {
        return score;
    }

    void GameOver()
    {
        // TODO
    }
}
