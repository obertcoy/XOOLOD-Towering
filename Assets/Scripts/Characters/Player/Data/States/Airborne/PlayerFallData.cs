using System;
using UnityEngine;

[Serializable]
public class PlayerFallData
{
    [field: SerializeField] [field: Range(1f, 15f)] public float FallSpeedLimit { get; private set; } = 15f;
    [field: SerializeField] [field: Range(0f, 100f)] public float HardFallTreshold { get; private set; } = 3f;
    [field: SerializeField] [field: Range(0f, 100f)] public float FallDamage { get; private set; } = 10f;

}
