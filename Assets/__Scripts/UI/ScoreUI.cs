using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText; 

    // Start is called before the first frame update
    void Start()
    {
        Score.instance.UpdateScore.AddListener(UpdateScoreUI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateScoreUI(float score)
    {
        scoreText.text = score.ToString();
    }
}
