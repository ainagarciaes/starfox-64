using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public CameraShake cs;
    private static LevelManager _instance;
    GameObject audioWin;
    bool audioWinPlayed = false;
    GameObject audioLose;
    bool audioLosePlayed = false;

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
    [SerializeField] Image turboNormal;
    [SerializeField] Image turboGod;
    [SerializeField] GameObject Audio;
    [SerializeField] Cinemachine.CinemachineDollyCart cart;
    [SerializeField] Sprite stc;
    [SerializeField] Sprite stg;

    private AudioManager am;
    public static bool IsPaused = false;
    private bool IsGameOver = false;
    private bool GodMode = false;

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
        GameGUI.GetComponent<ToggleGameModeGUI>().ToggleGodMode(GodMode);
        DamageGUI.SetActive(true);
        am = Audio.GetComponent<AudioManager>();
        am.PlaySound();
        audioWin = GameObject.Find("AudioWin");
        audioLose = GameObject.Find("AudioLose");
    }

    // Update is called once per frame
    void Update()
    {
        // update turbo bar
        float f = cart.GetComponent<Turbo>().GetTurboValue();
        turboNormal.sprite = stc;
        turboGod.sprite = stc;
        if (f < 0)
        {
            turboNormal.sprite = stg;
            turboGod.sprite = stg;
            f = f * -1;
        }
        turboGod.fillAmount = f;
        turboNormal.fillAmount = f;
        
        IsPaused = PauseMenu.activeInHierarchy;
        Cursor.visible = IsPaused;
        if (!PauseMenu.activeInHierarchy)
        {
            GameGUI.SetActive(true);
            DamageGUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.G))
            {
                GodMode = !GodMode;
                GameGUI.GetComponent<ToggleGameModeGUI>().ToggleGodMode(GodMode);
            }
        }
        // GO TO PAUSE
        if (!IsPaused && (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)))
        {
            GameOverMenu.SetActive(false);
            GameWinMenu.SetActive(false);
            GameGUI.SetActive(false);
            DamageGUI.SetActive(false);
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        // BACK FROM PAUSE
        else if (IsPaused && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)))
        {
            Time.timeScale = 1;
            PauseMenu.GetComponent<PaseMenuController>().ResumeGame();
            GameOverMenu.SetActive(false);
            GameWinMenu.SetActive(false);
            GameGUI.SetActive(true);
            DamageGUI.SetActive(false);
            PauseMenu.SetActive(false);
            IsPaused = false;
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
            DamageGUI.GetComponent<DamageAnim>().StartDamageAnimation();
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
            //shakeCamera sc = this.GetComponent<shakeCamera>();
            //sc.Shake();
            StartCoroutine(cs.Shake(0.5f, 0.1f));
            if (hitpoints <= 0)
            {
                healthBar.fillAmount = 0;
                if (!GodMode)
                {
                    GameOver();
                }
            }
            else
            {
                DamageGUI.GetComponent<DamageAnim>().StartDamageAnimation();
                healthBar.fillAmount = (float)hitpoints / max_hitpoints;
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
        if (!IsGameOver)
        {
            if (!audioLosePlayed)
                audioLose.GetComponent<AudioManager>().PlaySound();
            IsGameOver = true;
            PauseMenu.SetActive(false);
            GameOverMenu.SetActive(true);
            GameWinMenu.SetActive(false);
            GameGUI.SetActive(false);
            DamageGUI.SetActive(false);
            Invoke("RestartLevel", 3);
        }
    }

    public void GameWin()
    {
        if (!IsGameOver)
        {
            if (!audioWinPlayed)
                audioWin.GetComponent<AudioManager>().PlaySound();
            PauseMenu.SetActive(false);
            GameOverMenu.SetActive(false);
            GameWinMenu.SetActive(true);
            GameGUI.SetActive(false);
            DamageGUI.SetActive(false);
            Invoke("LoadNextLevel", 3);
        }
    }

    public void SetBurning(bool b)
    {
        burning = b;
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetRotation(bool rot)
    {
        roll = rot;
    }
}
