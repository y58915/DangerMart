using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using TMPro;


//maybe timer will merge into level controller and this will only be an UI script
public class CountdownTimerUI : MonoBehaviour
{
    private float currentTime;
    [SerializeField] Text timerText;

    // Update is called once per frame
    void Update()
    {
        currentTime = LevelController.instance.GetLevelTime();
        int minute = (int)(currentTime / 60);
        int seconds = (int)(currentTime % 60);

        if (minute == 5 && seconds == 0)
        {
            StartCoroutine(Alert());
        }

        if (minute == 1 && seconds == 0)
        {
            StartCoroutine(Alert());
        }

        string secondsTextFormat = (seconds >= 10) ? seconds.ToString() : "0" + seconds.ToString();

        timerText.text = minute.ToString() + ":" + secondsTextFormat;
    }

    IEnumerator Alert()
    {
        timerText.color = Color.red;

        yield return new WaitForSeconds(1);

        timerText.color = Color.white;
    }
}
