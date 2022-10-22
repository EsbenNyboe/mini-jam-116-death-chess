using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private FMODUnity.StudioEventEmitter eventEmitter;
    public static AudioManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void MusicTrigger(float intensity)
    {
        eventEmitter.EventInstance.setParameterByName("Intensity", intensity);
    }
}
