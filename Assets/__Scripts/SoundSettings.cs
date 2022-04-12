using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SoundSettings : MonoBehaviour
{
    const int BGM = 0;
    const int SFX = 1;
    // static float[] volume = {1.0f , 1.0f};
    static bool mute = false;
    // public AudioClip[] clips;

    AudioSource[] sources;
    SoundButton soundButton;

    public UnityEvent onSoundToggle;

    #region Singleton
    public static SoundSettings instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        sources = GetComponentsInChildren<AudioSource>(); // the order is BGM, SFX
        // ChangeBGMVolume(volume[BGM]);
        // ChangeSFXVolume(volume[SFX]);

        onSoundToggle.AddListener(ToggleSound);

        // Slider[] sliders = GetComponentsInChildren<Slider>(true);
        // sliders[BGM].value = volume[BGM];
        // sliders[SFX].value = volume[SFX];
    }

    void ToggleSound(){
        mute = !mute;
        for (int i = 0; i < 2; i++)
        {
            sources[i].mute = mute;
        }
    }

    // void ChangeVolume(int type, float num){
    //     volume[type] = num;
    //     sources[type].volume = num;
    // }

    // public void ChangeBGMVolume(float num){
    //     ChangeVolume(BGM, num);
    // }
    // public void ChangeSFXVolume(float num){
    //     ChangeVolume(SFX, num);
    // }
}
