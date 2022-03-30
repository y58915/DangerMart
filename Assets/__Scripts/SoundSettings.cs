using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundSettings : MonoBehaviour
{
    [Range(0, 1)] public float bgmVolume = 1.0f;
    [Range(0, 1)] public float sfxVolume = 1.0f;

    AudioSource[] sources;
    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        sources = GetComponentsInChildren<AudioSource>(); // the order is BGM, SFX
    }

    // Update is called once per frame
    void Update()
    {
        sources[0].volume = bgmVolume;
        sources[1].volume = sfxVolume;
    }
}
