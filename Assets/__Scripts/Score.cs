using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using TMPro;

public class Score : MonoBehaviour
{
    float currentScore = 0;
    public static Score Instance { get { return instance; } }

    [SerializeField] TextMeshProUGUI scoreText;

    private static Score instance;


    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
        {
            Score.Instance.currentScore = 0;
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
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
