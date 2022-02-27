using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRatingUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStar(int num = 1){
        foreach (Transform child in transform)
        {
            if (num > 0){
                child.gameObject.SetActive(true);
                num -= 1;
            } else{
                break;
            }
                
        }
    }
}
