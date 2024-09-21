using System;
using UnityEngine;

[Serializable]
public class EnemyFlyingData
{
    [field: SerializeField] [field: Range(1f, 5f)] public float SpeedModifier { get; private set; } = 1.5f;

}
