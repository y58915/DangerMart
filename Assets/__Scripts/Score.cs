using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using TMPro;

public class Score : MonoBehaviour
{
    float currentScore = 0;

    [SerializeField] TextMeshProUGUI scoreText;         //remove it. UI will be managed together


    #region Singleton
    public static Score instance;

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
        currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + currentScore.ToString();
    }

    public void AddScore(float score)
    {
        currentScore += score;
        AnalyticsResult analyticsResult = Analytics.CustomEvent(
            "High Score",
            new Dictionary<string, object>
            {
                {
                    "Score", currentScore
                }
            });
        Debug.Log("Score result: " + analyticsResult);
    }

    public void SubtractScore(float score)
    {
        currentScore -= score;
    }

    public float GetScore()
    {
        return currentScore;
    }

    public void SetScore(float score)
    {
        currentScore = score;

    }

}
