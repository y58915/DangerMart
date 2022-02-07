using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float currentTime = 0f;

    public static CountdownTimer Instance { get { return instance; } }

    [SerializeField] TextMeshProUGUI timerText;

    private static CountdownTimer instance;

    private bool timerStart = true;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            CountdownTimer.Instance.currentTime = this.currentTime;
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
        if (timerStart)
        {
            currentTime -= 1 * Time.deltaTime;

            if (currentTime < 0)
            {
                currentTime = 0;
                timerStart = false;
            }

            int minute = (int)(currentTime / 60);
            int seconds = (int)(currentTime % 60);

            string secondsTextFormat = (seconds > 10) ? seconds.ToString() : "0" + seconds.ToString();

            timerText.text = minute.ToString() + ":" + secondsTextFormat;
        }
    }

    public void StartTimer()
    {
        timerStart = true;
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
