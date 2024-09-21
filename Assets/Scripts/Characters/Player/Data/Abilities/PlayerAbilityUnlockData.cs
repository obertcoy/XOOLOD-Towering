using System;
using UnityEngine;

[Serializable]
public class PlayerAbilityUnlockData
{

    [field: SerializeField] [field: Range(1, 100)] public int Level { get; private set; }
    [field: SerializeField] [field: Range(0, 9999999)] public int Gold { get; private set; }
    [field: SerializeField] public Boolean Unlocked { get; private set; } = false;

    public void UnlockAbility()
    {
        Unlocked = true;
    }

}
