using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField]float currentScore = 0;
    [SerializeField]float maxScore = 2000;
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
        int temp = GameManager.instance.GetMaxScore();

        if (temp != 0)
            maxScore = temp;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddScore(float score)
    {
        currentScore += score;

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

    public float GetMaxScore()
    {
        return maxScore;
    }

    public void SetScore(float score)
    {
        currentScore = score;

    }

    public int GetMedal()
    {
        if (currentScore/ maxScore >= 0.9)
        {
            return 3;
        }
        if (currentScore / maxScore >= .6f)
        {
            return 2;
        }
        if (currentScore / maxScore >= .3f)
        {
            return 1;
        }

        return 0;
    }
}
