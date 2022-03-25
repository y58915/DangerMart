using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] Image scoreImage;
    [SerializeField] float maxScore = 2000;

    // Start is called before the first frame update
    void Start()
    {
        Score.instance.UpdateScore.AddListener(UpdateScoreUI);
        UpdateScoreUI(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateScoreUI(float score)
    {
        float temp = Mathf.Clamp(score / maxScore, 0, 1);
        scoreImage.fillAmount = temp;
    }
}
