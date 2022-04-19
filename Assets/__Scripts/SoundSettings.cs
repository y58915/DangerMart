using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SoundSettings : MonoBehaviour
{

    AudioSource[] sources;
    SoundButton soundButton;

    // Start is called before the first frame update
    void Start()
    {
        sources = GetComponentsInChildren<AudioSource>(); // the order is BGM, SFX
        // ChangeBGMVolume(volume[BGM]);
        // ChangeSFXVolume(volume[SFX]);

        for (int i = 0; i < 2; i++)
        {
            sources[i].mute = !GameManager.instance.GetSound();
        }

        // Slider[] sliders = GetComponentsInChildren<Slider>(true);
        // sliders[BGM].value = volume[BGM];
        // sliders[SFX].value = volume[SFX];
    }

    public void ToggleSound(bool state){
        for (int i = 0; i < 2; i++)
        {
            sources[i].mute = !state;
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
