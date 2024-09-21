using System;
using UnityEngine;

[Serializable]
public class PlayerPassiveAbilityData
{

    [field: SerializeField] [field: Range(1f, 10f)] public float Multiplier { get; set; }


}
