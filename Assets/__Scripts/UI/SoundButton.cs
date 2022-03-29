using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] Image soundButtonImage;
    [SerializeField] Button soundButton;
    [SerializeField] Sprite soundOnSprite;
    [SerializeField] Sprite soundOffSprite;

    public bool soundOn = true;

    // Start is called before the first frame update
    void Start()
    {
        soundButton.onClick.AddListener(ToggleSound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ToggleSound()
    {
        if (soundOn)
        {
            //Sound Off
            soundButtonImage.sprite = soundOffSprite;
            soundOn = false;
        }
        else
        {
            //Sound On
            soundButtonImage.sprite = soundOnSprite;
            soundOn = true;
        }
    }
}
