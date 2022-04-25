using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarRatingUI : MonoBehaviour
{
    [SerializeField] Sprite fullStar;
    [SerializeField] Sprite emptyStar;

    Image[] starList;

    // Start is called before the first frame update
    void Start()
    {
        starList = GetComponentsInChildren<Image>();
        //foreach (var child in starList)
        //{
        //    child.gameObject.SetActive(false);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStar(int num = 1)
    {
        foreach (var child in starList)
        {
            if (num > 0)
            {
                //child.gameObject.SetActive(true);
                child.sprite = fullStar;
                num -= 1;
            } 
            else
            {
                //child.gameObject.SetActive(true);
                child.sprite = emptyStar;
            }
        }
    }
}
