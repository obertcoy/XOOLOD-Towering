using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "Custom/Characters/NPC")]
public class NPCSO : ScriptableObject
{
    [field: SerializeField] public string DisplayName { get; private set; }
    [field: SerializeField] public string DisplayType { get; private set; }
    [field: SerializeField] public NPCTypeEnum Type { get; private set; }
    [field: SerializeField] public float LookRange { get; private set; } = 5f;

    [field: SerializeField] public float RotationSpeed { get; private set; } = 0.25f;

}
