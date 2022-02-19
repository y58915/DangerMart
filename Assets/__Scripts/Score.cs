using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;
using TMPro;

public class Score : MonoBehaviour
{
    float currentScore = 0;

    [HideInInspector] public UnityEvent<float> UpdateScore;

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

        UpdateScore.Invoke(currentScore);
    }

    public void SubtractScore(float score)
    {
        currentScore -= score;

        UpdateScore.Invoke(currentScore);
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
