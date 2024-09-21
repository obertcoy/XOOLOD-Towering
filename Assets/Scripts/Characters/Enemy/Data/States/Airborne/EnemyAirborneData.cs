using System;
using UnityEngine;

[Serializable]
public class EnemyAirborneData
{
    [field: SerializeField] public EnemyFlyingData FlyingData { get; private set; }

    [field: SerializeField] public bool EnableFlying { get; private set; }
}
