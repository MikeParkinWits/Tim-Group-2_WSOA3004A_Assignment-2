using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerMike : MonoBehaviour
{
    public int colour; 

    [Header("Inspector Variables")]
    public GameObject pauseMenu;
    public GameObject topBar;
    public Text highScoreText;
    public Text currentScoreText;

    [Header("Timer Variables")]
    public GameObject timerScreen;
    public Text timerText;
    private float timer = 3f;
    private bool loadTimer = false;

    public static bool isPause = false;

    private void Awake()
    {
        colour = Random.Range(0, 3);
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString("0");
    }

    // Start is called before the first frame update
    void Start()
    {
        
        Time.timeScale = 1;
        loadTimer = false;
        isPause = false;
    }

    // Update is called once per frame
    void Update()
    {

        currentScoreText.text = GrappleHookController.score.ToString("0");

        if (PlayerPrefs.GetInt("HighScore") < GrappleHookController.score)
        {
            PlayerPrefs.SetInt("HighScore", GrappleHookController.score);
            highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString("0");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause();
        }

        if (loadTimer)
        {
            Timer();
        }
        else
        {
            timerScreen.SetActive(false);
        }
    }

    public void OnPause()
    {
        if (isPause)
        {
            pauseMenu.SetActive(false);

            loadTimer = true;

        }
        else
        {
            pauseMenu.SetActive(true);
            topBar.SetActive(false);
            isPause = true;

            timer = 3f;

            Time.timeScale = 0;
        }
    }

    private void Timer()
    {
        timerScreen.SetActive(true);

        if (timer> 0f)
        {
            timer -= Time.unscaledDeltaTime;

            timerText.text = timer.ToString("0");
        }
        else
        {
            loadTimer = false;
            timerScreen.SetActive(false);

            topBar.SetActive(true);
            isPause = false;

            Time.timeScale = 1;
        }
    }

    public void LoadNewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
