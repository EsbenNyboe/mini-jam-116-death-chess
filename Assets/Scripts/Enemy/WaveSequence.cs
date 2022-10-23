using System;
using SfxSystem;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Serialization;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "WaveSequence", menuName = "New Wave Sequence")]
public class WaveSequence : ScriptableObject
{
    [OnValueChanged("SetWaveGroupStates")] 
    public bool showWaveGroups;
    [OnValueChanged("SetHideStates")] public bool showIndividualMenus;
    [ShowIf("showIndividualMenus", false)]
    [OnValueChanged("ResetMenus")] public bool resetMenus;

    [OnValueChanged("SetDisplayStates")] [EnumToggleButtons]
    public DisplayState displayState;

    private void ResetMenus()
    {
        if (resetMenus)
        {
            resetMenus = false;
            for (int i = 0; i < Waves.Length; i++)
            {
                Waves[i].showMenu = false;
                Waves[i].displayState = displayState;
            }
        }
    }
    private void SetDisplayStates()
    {
        for (int i = 0; i < Waves.Length; i++)
        {
            // Waves[i].displayState = displayState;
            if (showIndividualMenus == false)
            {
                Waves[i].displayState = displayState;
            }
        }
    }

    private void SetHideStates()
    {
        for (int i = 0; i < Waves.Length; i++)
        {
            Waves[i].hideState = showIndividualMenus;
            if (showIndividualMenus == false)
            {
                // Waves[i].showMenu = false;
            }
        }
    }

    private void SetWaveGroupStates()
    {
        int waveGroupIndex = 0;
        for (int i = 0; i < Waves.Length; i++)
        {
            if (Waves[i].newWaveGroup)
            {
                waveGroupIndex++;
                Waves[i].waveGroupIndex = waveGroupIndex;
                Waves[i].showWaveGroupIndex = showWaveGroups;
            }
        }
    }

    [OnValueChanged("SetWaveGroupStates", true)] 
    [OdinSerialize] public Wave[] Waves;

    [Serializable]
    public class Wave
    {
        [ShowIf("showWaveGroupIndex")]
        public int waveGroupIndex;

        [HideInInspector] public bool showWaveGroupIndex;
        
        [HideInInspector] public bool hideState;

        [EnumToggleButtons] [ShowIf("showMenu")] [OnValueChanged("SaveLocalDisplayState")]
        public DisplayState displayState;

        [HideInInspector] public DisplayState _displayStateCache;

        [HorizontalGroup("Group 1")] public GameObject enemy;

        [HorizontalGroup("Group 1")] [HideLabel] [ShowIf("hideState")]
        public bool showMenu;

        [ShowIf("@this.displayState == DisplayState.Count || this.displayState == DisplayState.Advanced")]
        public int count;

        [ShowIf("displayState", DisplayState.Advanced)]
        public float spawnWaitTime;

        // position
        [ShowIf("displayState", DisplayState.Advanced)]
        public SerialLogicHelper.SerialLogic serialLogic;
        
        [ShowIf("displayState", DisplayState.Advanced)]
        public int gridIndex = -1;
        
        // wave-transition mechanics
        [ShowIf("displayState", DisplayState.Advanced)]
        public bool waitForClear;

        [ShowIf("displayState", DisplayState.WaveGroups)]
        [OnValueChanged("ShowWaveGroupIndex")]
        public bool newWaveGroup;

        [ShowIf("displayState", DisplayState.Music)]
        public MusicTriggerOnWaveStart musicTriggerOnWaveStart;

        private void ShowWaveGroupIndex()
        {
            showWaveGroupIndex = newWaveGroup;
            if (newWaveGroup == false)
            {
                waveGroupIndex = 0;
            }
        }

        private void SaveLocalDisplayState()
        {
            _displayStateCache = displayState;
        }
    }

    public enum DisplayState
    {
        Simple,
        Count,
        Advanced,
        Music,
        WaveGroups
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