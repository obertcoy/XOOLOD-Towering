using System;
using UnityEngine;

[Serializable]
public class EnemyWalkData
{
    [field: SerializeField] [field: Range(0f, 2f)] public float SpeedModifier { get; private set; } = 0.225f;
    [field: SerializeField] [field: Range(1f, 50f)] public float PatrolRange { get; private set; } = 5f;
    [field: SerializeField] [field: Range(1, 10)] public int PatrolWaypointCount { get; private set; } = 5;
    [field: SerializeField] [field: Range(1f, 10f)] public float PatrolCooldown { get; private set; } = 3f;

}
