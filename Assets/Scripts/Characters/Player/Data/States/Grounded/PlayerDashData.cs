using System;
using UnityEngine;

[Serializable]
public class PlayerDashData
{
    [field: SerializeField] [field: Range(1f, 3f)] public float SpeedModifier { get; private set; } = 2f;
    [field: SerializeField] public PlayerRotationData RotationData { get; private set; }
    [field: SerializeField] [field: Range(0f, 2f)] public float ConsecutiveInterval { get; private set; } = 1f;
    [field: SerializeField] [field: Range(1, 10)] public int ConsesutiveLimit { get; private set; } = 2;
    [field: SerializeField] [field: Range(0f, 5f)] public float ConsecutiveCooldown { get; private set; } = 1.75f;
}
