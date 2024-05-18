using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu]

public class AudioManager : ScriptableObject
{
    // The list of sounds
    public List<SoundObject> soundList;

    // Used to hold the specific audio manager that is created for that scene
    GameObject audioInstance;
    
    // Creates the audio manager that will have the sources on it
    public void CreateManager()
    {
        audioInstance = Instantiate(new GameObject("Audio Manager"));

        // Adds one audio source for each clip
        foreach (SoundObject s in soundList) 
        {
            s.source = audioInstance.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    // Plays the sound of the fed in clip
    public void playSound(string sound)
    {
        foreach (SoundObject s in soundList) 
        { 
            if (s.name == sound)
            {
                s.source.Play();

                break;
            }
        }
    }

    public void stopSound(string sound) 
    {
        foreach (SoundObject s in soundList)
        {
            if (s.name == sound)
            {
                s.source.Stop();

                break;
            }
        }
    }

}
