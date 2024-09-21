using System;
using UnityEngine;

[Serializable]
public class PlayerLockData
{
    [field: SerializeField] public float FarLockDistance { get; private set; } = 15f;
    [field: SerializeField] public float NearLockDistance { get; private set; } = 7f;
    [field: SerializeField] public float HeightOffset { get; private set; } = 0.5f;
    [field: SerializeField] public float ColorChangeSmoothness { get; private set; } = 5f;
}
