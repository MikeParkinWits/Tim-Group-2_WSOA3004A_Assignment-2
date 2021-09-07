using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerMike : MonoBehaviour
{

    [Header("Inspector Variables")]
    public GameObject pauseMenu;
    public GameObject topBar;

    [Header("Timer Variables")]
    public GameObject timerScreen;
    public Text timerText;
    private float timer = 3f;
    private bool loadTimer = false;

    public static bool isPause = false;



    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        loadTimer = false;
    }

    // Update is called once per frame
    void Update()
    {
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
