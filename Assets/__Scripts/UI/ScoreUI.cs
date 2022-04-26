using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] Image scoreImage;
    RectTransform rt;

    // Start is called before the first frame update
    void Start()
    {
        Score.instance.UpdateScore.AddListener(UpdateScoreUI);

        rt = scoreImage.GetComponent<RectTransform>();

        UpdateScoreUI(0);
    }


    void UpdateScoreUI(float score)
    {
        float temp = Mathf.Clamp(score / Score.instance.GetMaxScore(), 0, 1) * 0.9f + 0.1f;

        rt.anchorMax = new Vector2(rt.anchorMax.x, temp);
    }


}
