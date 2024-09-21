using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Active", menuName = "Custom/Characters/Ability/Enemy/Active")]
public class EnemyActiveAbilitySO : EnemyAbilitySO
{
    [field: Header("Active Ability")]
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public EnemyActiveAbilityData AbilityData { get; set; }
    [field: SerializeField] public float SpawnDistance { get; private set; } = 3f;
    [field: SerializeField] public bool Moving { get; private set; }
    [field: SerializeField] public bool AlreadySetup { get; private set; }

    [field: Header("Moving")]
    [field: SerializeField] public float MovingSpeed { get; private set; } = 5f;
    [field: SerializeField] public float MaxDistance { get; private set; } = 200f;

    public void ActivateSetupAbility(GameObject prefab)
    {
        if (!AlreadySetup) return;

        Prefab = prefab;

        if(Prefab.TryGetComponent(out ISetupAbility ability))
        {
            ability.ActivateAbility();
        }
    }
    public void DeactivateSetupAbility()
    {
        if (!AlreadySetup) return;

        if (Prefab.TryGetComponent(out ISetupAbility ability))
        {
            ability.DeactivateAbility();
        }
    }


}
