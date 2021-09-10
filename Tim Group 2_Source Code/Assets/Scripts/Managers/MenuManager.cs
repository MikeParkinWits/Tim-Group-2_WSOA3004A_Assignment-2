using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public LineRenderer lineRenderer;
    public GameObject ball;

    private bool creditsLoaded = false;
    public GameObject creditsScreen;

    // Start is called before the first frame update
    void Start()
    {
        creditsLoaded = false;

        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(1, ball.transform.position);
    }

    public void LoadCredits()
    {
        if (creditsLoaded)
        {
            ball.SetActive(true);
            creditsScreen.SetActive(false);

            creditsLoaded = false;
        }
        else
        {
            ball.SetActive(false);
            creditsScreen.SetActive(true);

            creditsLoaded = true;
        }
    }
}
