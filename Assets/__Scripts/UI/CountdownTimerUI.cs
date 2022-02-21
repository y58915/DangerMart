using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Analytics;
using TMPro;


//maybe timer will merge into level controller and this will only be an UI script
public class CountdownTimerUI : MonoBehaviour
{
    public float currentTime;
    [SerializeField] TextMeshProUGUI timerText;

    private bool timerPaused = false;
    private Score scoreReference;

    [SerializeField]
    private GameObject gameOverScreen;

    private void Start()
    {
        currentTime = LevelController.instance.GetLevelTimer();
        scoreReference = GameObject.Find("LevelController").GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerPaused)
        {
            currentTime -= Time.deltaTime;

            if (currentTime < 0)
            {
                currentTime = 0;
                timerPaused = true;
                
                // End the round 
                Time.timeScale = 0.0f;
                gameOverScreen.SetActive(true);

                // Add analytics for Total Score
                AnalyticsResult analyticsResult = Analytics.CustomEvent("High Score", new Dictionary<string, object> { { "Score", scoreReference.currentScore } });
                Debug.Log("High Score " + analyticsResult);

                // TODO: Add Analytics for Total Lists Completed
                //AnalyticsResult analyticsResult = Analytics.CustomEvent("Total Lists Completed", new Dictionary<string, object> { { "Lists", *something goes here* } });
            }

            int minute = (int)(currentTime / 60);
            int seconds = (int)(currentTime % 60);

            string secondsTextFormat = (seconds > 10) ? seconds.ToString() : "0" + seconds.ToString();

            timerText.text = minute.ToString() + ":" + secondsTextFormat;
        }
    }

    public void ToggleTimerPause()
    {
        timerPaused = !timerPaused;
    }

    public void AddToCurrentTime(float time)
    {
        currentTime += time;
    }

    public bool IsCurrentTimeZero()
    {
        return currentTime == 0;
    }
}
