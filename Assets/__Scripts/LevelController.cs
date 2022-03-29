using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Analytics;

public class LevelController : MonoBehaviour
{
    [SerializeField] private float levelTime;
    [SerializeField] private int level;

    public UnityEvent gameOverEvent;

    public bool gamePaused { get { return pauseCount != 0; } }
    private int pauseCount;

    private bool noGameOver = false;

    [SerializeField] GameObject medals;


    #region Singleton
    public static LevelController instance;

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


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        LevelTimer();
    }

    private void LevelTimer()
    {
        if (!gamePaused)
        {
            levelTime -= Time.deltaTime;

            if (levelTime < 0)
            {
                levelTime = 0;

                if (!noGameOver)
                {
                    SetPause(true);
                    GetAnalytics();
                    gameOverEvent.Invoke();
                    GameManager.instance.SetScore(level, (int)Score.instance.GetScore());
                }
            }

        }
    }

    private void GetAnalytics()
    {
        // Add analytics for High Score
        AnalyticsResult analyticsResult_Score = 
            Analytics.CustomEvent("High Score", new Dictionary<string, object> 
            { { "Score", Score.instance.currentScore } });
        // Debug.Log("High Score: " + analyticsResult_Score);

        // Add analytics for Total Lists Complete
        AnalyticsResult analyticsResult_Lists = 
            Analytics.CustomEvent("Total Lists Complete", new Dictionary<string, object> 
            { { "Lists", ShoppingListManager.instance.listsCompleted } });
        // Debug.Log("Total Lists Complete: " + analyticsResult_Lists);

        float dist = GameObject.FindGameObjectsWithTag("Player")[0].
            GetComponent<CharacterControl>().distance;

        AnalyticsResult analyticsResult_Dist = 
            Analytics.CustomEvent("Distance Traveled", new Dictionary<string, object>
            { { "Distance", dist } });
        // Debug.Log("Total Distance Traveled: " + analyticsResult_Dist);

        int clicks = GameObject.FindGameObjectsWithTag("Player")[0].
            GetComponent<CharacterControl>().mouse_clicks;

        AnalyticsResult analyticsResult_Clicks = 
            Analytics.CustomEvent("Mouse Clicks", new Dictionary<string, object> 
            { { "Clicks", clicks } });
        // Debug.Log("Total Mouse Clicks: " + analyticsResult_Clicks);
    }

    #region Getter and Setter
 
    public float GetLevelTime()
    {
        return levelTime;
    }
    public void SetNoGameOver(bool tf)
    {
        noGameOver = tf;
    }

    public void SetPause(bool tf)
    {
        if (tf)
            pauseCount++;
        else
            pauseCount--;
    }

    public void CleanPause()
    {
        pauseCount = 0;
    }
    #endregion

}
