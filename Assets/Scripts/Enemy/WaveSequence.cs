using System;
using SfxSystem;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveSequence", menuName = "New Wave Sequence")]
public class WaveSequence : ScriptableObject
{
    [Serializable]
    public class Wave
    {
        public GameObject enemy;
        public int count;
        public float spawnWaitTime;
        public SerialLogicHelper.SerialLogic serialLogic;
        public bool waitForClear;
        [Range(-1,1)]
        public float musicIntensity = -1;
    }

    public Wave[] Waves;

    public enum MusicTriggerOnStart
    {
        None,
        HighA
    }
    public enum MusicTriggerOnClear
    {
        None,
        LowA,
        LowB,
        LowC,
        LowD
    }
}