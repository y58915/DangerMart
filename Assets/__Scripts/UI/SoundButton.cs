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
    Button btn;
    SoundSettings soundSettings;

    // Start is called before the first frame update
    void Start()
    {
        soundSettings = GameObject.Find("SoundManager").GetComponent<SoundSettings>();
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ToggleSound);

        soundOn = GameManager.instance.GetSound();
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
        GameManager.instance.SetSound(soundOn);
        UpdateSprite();
        soundSettings.ToggleSound(soundOn);
    }
}
