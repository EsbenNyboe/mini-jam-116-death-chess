using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExplosionType", menuName = "Add Explosion Asset")]
public class ExplosionType : ScriptableObject
{
    public float radius = 5f;
    public float power = 10f;
    public float upwardsModifier = 3f;
}
