using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct NamedAudioClip
{
    public AudioClip clip;
    public string name;

    public NamedAudioClip(AudioClip clip = null, string name = ""){
        this.clip = clip;
        this.name = name;
    }
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private AudioSource source;
    public NamedAudioClip[] clips;

    private void Awake()
    {
        Debug.Assert(clips != null, this);
        Debug.Assert(clips.Length != 0, this);

        source = GetComponent<AudioSource>();
    }

    private NamedAudioClip Find(string name){
        foreach (NamedAudioClip clip in clips)
        {
            if (clip.name == name){
                return clip;
            }
        }
        return new NamedAudioClip();
    }

// the complete list of playable audio will be available in editor
    public void Play(string name)
    {
        NamedAudioClip clip = Find(name);
        if (clip.name != ""){
            source.PlayOneShot(clip.clip, source.volume);
        }
    }
}
