using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerAbilitiesNameEnum
{
    HollowRed,
    HorizontalSlash,
    LaserRain,
    MeteorShower,
    RedEnergyExplosion
}

public class PlayerAbilitySO : AbilitySO
{

    [field: SerializeField] public PlayerAbilitiesNameEnum Name { get; private set; }

    [field: Header("Unlock Requirement")]
    [field: SerializeField] public PlayerAbilityUnlockData UnlockData { get; private set; }

    public void UnlockAbility()
    {
        UnlockData.UnlockAbility();
    }

}
