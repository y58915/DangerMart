using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController_Tutorial : LevelController
{
    [SerializeField]
    private Level1Tutorial level1Tutorial;

    private void Update()
    {
        if (!noGameOver && Score.instance.currentScore >= Score.instance.GetMaxScore())
        {
            noGameOver = true;
            level1Tutorial.FinishTutorial();

            SetPause(true);
            //GetAnalytics();
            gameOverEvent.Invoke();
            GameManager.instance.SetScore(level, (int)Score.instance.GetScore(), Score.instance.GetMedal());
        }
    }
}
