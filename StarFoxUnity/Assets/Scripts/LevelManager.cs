﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;

    public static LevelManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject GameOverMenu;
    [SerializeField] GameObject GameWinMenu;
    [SerializeField] GameObject GameGUI;
    [SerializeField] GameObject DamageGUI;
    [SerializeField] Image healthBar;

    public static bool IsPaused = false;
    private int hitpoints = 100; // per posar algo, idk es pot adaptar després
    private int max_hitpoints = 100;
    private int score = 0;
    private bool roll = false;

    //dmg per second variables
    private bool burning = false;
    private float sumDelta = 0f;

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
        IsPaused = PauseMenu.activeInHierarchy;
        Cursor.visible = IsPaused;
        if (!PauseMenu.activeInHierarchy)
        {
            GameGUI.SetActive(true);
        }
        // show pause menu
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameOverMenu.SetActive(false);
            GameWinMenu.SetActive(false);
            GameGUI.SetActive(false);
            DamageGUI.SetActive(false);
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
        }

        // rotation code
        else if (!roll && Input.GetKeyDown(KeyCode.R))
        {
            roll = true;
            // todo passar aqui el codi del roll i afegir-hi el que calgui per settejar el roll a false quan acabi
        }

        // burning code
        if (burning)
        {
            sumDelta += Time.deltaTime;
            if (sumDelta >= 0.1f)
            {
                sumDelta = 0f;
                UpdateHitPoints(1, 0);
            }
        }
    }

    public void UpdateHitPoints(int damage, int type) // 0: damage per projectils, 1: colisio...
    {
        if (!(roll && type == 0)) // roll evita damage per colisio
        {
            print("taking damage equal to: " + damage);
            hitpoints -= damage;
            if (hitpoints <= 0)
            {
                healthBar.fillAmount = 0;
                GameOver();
            }
            else
            {
                healthBar.fillAmount = (float) hitpoints / max_hitpoints;
            }
        }
    }

    public void UpdateScore(int scoreIncr)
    {
        score += scoreIncr;
    }

    public int GetScore()
    {
        return score;
    }

    void GameOver()
    {
        PauseMenu.SetActive(false);
        GameOverMenu.SetActive(true);
        GameWinMenu.SetActive(false);
        GameGUI.SetActive(false);
        DamageGUI.SetActive(false);
    }

    public void SetBurning(bool b)
    {
        burning = b;
    }
}