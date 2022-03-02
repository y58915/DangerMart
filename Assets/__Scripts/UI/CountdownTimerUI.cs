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

    [SerializeField]
    private GameObject gameOverScreen;

    private bool timerPaused = false;
    private Score scoreReference;
    private ShoppingListManager listManager;

    public bool noTimeOver = false;


    #region Singleton
    public static CountdownTimerUI instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion


    private void Start()
    {
        currentTime = LevelController.instance.GetLevelTimer();

        scoreReference = GameObject.Find("LevelController").GetComponent<Score>();
        listManager = GameObject.Find("LevelController").GetComponent<ShoppingListManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (noTimeOver) return;

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

                // Add analytics for High Score
                AnalyticsResult analyticsResult_Score = Analytics.CustomEvent("High Score", new Dictionary<string, object> { { "Score", scoreReference.currentScore } });
                // Debug.Log("High Score: " + analyticsResult_Score);

                // Add analytics for Total Lists Complete
                AnalyticsResult analyticsResult_Lists = Analytics.CustomEvent("Total Lists Complete", new Dictionary<string, object> { { "Lists", listManager.listsCompleted } });
                // Debug.Log("Total Lists Complete: " + analyticsResult_Lists);

                float dist = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<CharacterControl>().distance;
                AnalyticsResult analyticsResult_Dist = Analytics.CustomEvent("Distance Traveled", new Dictionary<string, object> { { "Distance", dist } });
                // Debug.Log("Total Distance Traveled: " + analyticsResult_Dist);

                int clicks = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<CharacterControl>().mouse_clicks;
                AnalyticsResult analyticsResult_Clicks = Analytics.CustomEvent("Mouse Clicks", new Dictionary<string, object> { { "Clicks", clicks } }) ;
                // Debug.Log("Total Mouse Clicks: " + analyticsResult_Clicks);
            }

            int minute = (int)(currentTime / 60);
            int seconds = (int)(currentTime % 60);

            string secondsTextFormat = (seconds >= 10) ? seconds.ToString() : "0" + seconds.ToString();

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
