using System;
using SfxSystem;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "WaveSequence", menuName = "New Wave Sequence")]
public class WaveSequence : ScriptableObject
{
    [OnValueChanged("SetHideStates")]
    public bool showIndividualMenus;
    [OnValueChanged("SetDisplayStates")]
    [EnumToggleButtons] public DisplayState displayState;

    private void SetDisplayStates()
    {
        for (int i = 0; i < Waves.Length; i++)
        {
            Waves[i].displayState = displayState;
        }
    }
    private void SetHideStates()
    {
        for (int i = 0; i < Waves.Length; i++)
        {
            Waves[i].hideState = showIndividualMenus;
            Waves[i].showMenu = false;
        }
    }
    
    [OdinSerialize] public Wave[] Waves;

    [Serializable]
    public class Wave
    {
        [HideInInspector]
        public bool hideState;
        [EnumToggleButtons]
        [ShowIf("@this.hideState || this.showMenu")]
        public DisplayState displayState;

        [HorizontalGroup("Group 1")]
        public GameObject enemy;
        [HorizontalGroup("Group 1")] [HideLabel] [HideIf("hideState")]
        public bool showMenu;
        [HideIf("@this.displayState == DisplayState.Simple || this.displayState == DisplayState.Music")]
        public int count;
        [ShowIf("displayState", DisplayState.Advanced)]
        public float spawnWaitTime;
        [ShowIf("displayState", DisplayState.Advanced)]
        public SerialLogicHelper.SerialLogic serialLogic;
        [ShowIf("displayState", DisplayState.Advanced)]
        public bool waitForClear;
        [ShowIf("displayState", DisplayState.Music)]
        public MusicTriggerOnWaveStart musicTriggerOnWaveStart;
    }

    public enum DisplayState
    {
        Simple,
        Count,
        Advanced,
        Music
    }
}

public enum MusicTriggerOnWaveStart
{
    None,
    LowA,
    LowB,
    LowC,
    HighA,
    HighB,
    HighC,
    BossA
}