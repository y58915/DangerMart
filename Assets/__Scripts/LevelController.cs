using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private float levelTime;
    [SerializeField] protected int level;

    public UnityEvent gameOverEvent;

    public UnityEvent winEvent;
    public GameObject GameOverText;

    public bool gamePaused { get { return pauseCount != 0; } }
    private int pauseCount;

    protected bool noGameOver = false;

    private bool win = false;
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
            if(!win)
                levelTime -= Time.deltaTime;

            if (levelTime < 0)
            {
                levelTime = 0;

                if (!noGameOver)
                {
                    SetPause(true);
                    GetAnalytics();
                    GameOverText.GetComponent<Text>().text = "Times Up!";
                    gameOverEvent.Invoke();
                    GameManager.instance.SetScore(level, (int)Score.instance.GetScore(), Score.instance.GetMedal());
                }
            }

            if(Score.instance.GetScore() >= Score.instance.GetMaxScore() && !win)
            {
                win = true;
                SetPause(true);
                GetAnalytics();
                GameOverText.GetComponent<Text>().text = "Congratulation!";
                winEvent.Invoke();
                GameManager.instance.SetScore(level, (int)Score.instance.GetScore(), Score.instance.GetMedal());
            }
        }
    }

    private void GetAnalytics()
    {
        // Add analytics for High Score
        AnalyticsResult analyticsResult_Score = 
            Analytics.CustomEvent("High Score", new Dictionary<string, object> 
            { 
                { "Level", level }, 
                { "Score", Score.instance.GetScore() } 
            });
        // Debug.Log("High Score: " + analyticsResult_Score);

        // Add analytics for Total Lists Complete
        AnalyticsResult analyticsResult_Lists = 
            Analytics.CustomEvent("Total Lists Complete", new Dictionary<string, object> 
            { 
                { "Level", level }, 
                { "Lists", ShoppingListManager.instance.listsCompleted } 
            });
        // Debug.Log("Total Lists Complete: " + analyticsResult_Lists);

        float dist = GameObject.FindGameObjectsWithTag("Player")[0].
            GetComponent<CharacterControl>().distance;

        AnalyticsResult analyticsResult_Dist = 
            Analytics.CustomEvent("Distance Traveled", new Dictionary<string, object>
            { 
                { "Level", level }, 
                { "Distance", dist } 
            });
        // Debug.Log("Total Distance Traveled: " + analyticsResult_Dist);

        int clicks = GameObject.FindGameObjectsWithTag("Player")[0].
            GetComponent<CharacterControl>().mouse_clicks;

        AnalyticsResult analyticsResult_Clicks = 
            Analytics.CustomEvent("Mouse Clicks", new Dictionary<string, object> 
            {
                { "Level", level }, 
                { "Clicks", clicks } 
            });
        // Debug.Log("Total Mouse Clicks: " + analyticsResult_Clicks);
    }

    #region Getter and Setter
 
    public float GetLevelTime()
    {
        return levelTime;
    }

    public int GetLevel()
	{
        return level;
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
