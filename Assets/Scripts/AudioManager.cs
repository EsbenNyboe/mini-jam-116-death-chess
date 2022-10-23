using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private FMODUnity.StudioEventEmitter eventEmitter;
    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void MusicTrigger(MusicTriggerOnWaveStart intensityLabelEnum)
    {
        string intensityLabel = "";
        switch (intensityLabelEnum)
        {
            case MusicTriggerOnWaveStart.None:
                return;
            case MusicTriggerOnWaveStart.LowA:
                intensityLabel = "LowA";
                break;
            case MusicTriggerOnWaveStart.LowB:
                intensityLabel = "LowB";
                break;
            case MusicTriggerOnWaveStart.LowC:
                intensityLabel = "LowC";
                break;
            case MusicTriggerOnWaveStart.HighA:
                intensityLabel = "HighA";
                break;
            case MusicTriggerOnWaveStart.HighB:
                intensityLabel = "HighB";
                break;
            case MusicTriggerOnWaveStart.HighC:
                intensityLabel = "HighC";
                break;
            case MusicTriggerOnWaveStart.BossA:
                break;
        }

        if (intensityLabel == "")
        {
            return;
        }
        print(intensityLabel);
        eventEmitter.EventInstance.setParameterByNameWithLabel("IntensityLable", intensityLabel);
        // eventEmitter.EventInstance.setParameterByName("Intensity", intensity);
    }
}