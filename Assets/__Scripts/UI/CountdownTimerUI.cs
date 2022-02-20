using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;


//maybe timer will merge into level controller and this will only be an UI script
public class CountdownTimerUI : MonoBehaviour
{
    public float currentTime;
    [SerializeField] TextMeshProUGUI timerText;

    private bool timerPaused = false;

    private void Start()
    {
        currentTime = LevelController.instance.GetLevelTimer();
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
