using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{

    [field: SerializeField] public PlayerMovementAnimationData MovementAnimationData;
    [field: SerializeField] public PlayerCombatAnimationData CombatAnimationData;
    
    [field: SerializeField] [field : Range(0, 1f)] public float MinimumAnimationLayerWeight = 0f;
    [field: SerializeField] [field: Range(0, 1f)] public float MaximumAnimationLayerWeight = 1f;

    public enum PlayerAnimatorLayer
    {
        NonCombat = 0,
        Combat = 1,
        Dead = 2,
    }

    public void Initialize()
    {
        MovementAnimationData.Initialize();
        CombatAnimationData.Initialize();
    }
}
