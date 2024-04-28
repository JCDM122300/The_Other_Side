using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sound
{
    //Name for lookup of clip
    public string Name;

    //The sound clip
    public AudioClip clip;

    //Volume for specific clip
    [Range(0, 1)]
    public float Volume;

    //Loop setting for this sound
    public bool Loop;

    //Used to mix between 2D and 3D blend on Audio Source
    [Tooltip("0 -> 2D | 1 -> 3D")]
    [Range(0, 1)]
    public float SpatialBlend;

    //If Pitch should change each time sound is played, IF the current AudioSource Allows for it
    public bool RandomizePitch;

    [Range(0, 3)]
    public float MaxPitchOffset;

    //Sets source max hearing range
    public float MaxRange = 25;
}
