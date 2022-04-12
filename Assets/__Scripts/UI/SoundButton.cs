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
        UpdateSprite();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateSprite(){
        if (soundOn){
            soundButtonImage.sprite = soundOnSprite;
        } else{
            soundButtonImage.sprite = soundOffSprite;
        }
    }

    void ToggleSound()
    {
        soundOn = !soundOn;
        UpdateSprite();
        SoundSettings.instance.onSoundToggle.Invoke();
    }
}
