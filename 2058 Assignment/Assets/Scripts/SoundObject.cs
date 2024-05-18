using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[System.Serializable]

public class SoundObject
{
    public string name;

    public AudioClip clip;

    [Range(0f,1f)]
    public float volume;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
