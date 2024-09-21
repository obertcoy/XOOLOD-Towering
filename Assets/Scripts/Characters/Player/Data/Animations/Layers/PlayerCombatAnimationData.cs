using System;
using UnityEngine;

[Serializable]
public class PlayerCombatAnimationData
{
    [Header("State Group Paramater Names")]

    [field: SerializeField] private String combatParameterName = "Combat";
    [field: SerializeField] private String abilityParamaterName = "Ability";

    [Header("Sword Parameter Names")]

    [field: SerializeField] private String drawingSwordTriggerName = "DrawingSword";
    [field: SerializeField] private String sheatingSwordTriggerName = "SheatingSword";

    [Header("Basic Attack Parameter Name")]

    [field: SerializeField] private String basicAttackTriggerParameterName = "BasicAttackTrigger";
    [field: SerializeField] private String basicAttackParameterName = "BasicAttack";

    [Header("Ability Parameter Name")]

    [field: SerializeField] private String horizontalSlashParameterName = "isHorizontalSlash";
    [field: SerializeField] private String hollowRedParameterName = "isHollowRed";
    [field: SerializeField] private String laserRainParameterName = "isLaserRain";
    [field: SerializeField] private String meteorShowerParameterName = "isMeteorShower";
    [field: SerializeField] private String redEnergyExplosionParameterName = "isRedEnergyExplosion";



    public int CombatParameterHash { get; private set; }
    public int AbilityParameterHash { get; private set; }

    public int DrawingSwordParameterHash { get; private set; }
    public int SheatingSwordParameterHash { get; private set; }

    public int OutwardSlashParameterHash { get; private set; }
    public int InwardSlashParameterHash { get; private set; }
    public int ThrustSlashParameterHash { get; private set; }
    public int BasicAttackParameterHash { get; private set; }
    public int BasicAttackTriggerParameterHash { get; private set; }

    public int HorizontalSlashParameterHash { get; private set; }
    public int HollowRedParamaterHash { get; private set; }
    public int LaserRainParameterHash { get; private set; }
    public int MeteorShowerParameterHash { get; private set; }
    public int RedEnergyExplosionParameterHash { get; private set; }

    public void Initialize()
    {
        CombatParameterHash = Animator.StringToHash(combatParameterName);
        AbilityParameterHash = Animator.StringToHash(abilityParamaterName);

        DrawingSwordParameterHash = Animator.StringToHash(drawingSwordTriggerName);
        SheatingSwordParameterHash = Animator.StringToHash(sheatingSwordTriggerName);

        BasicAttackParameterHash = Animator.StringToHash(basicAttackParameterName);
        BasicAttackTriggerParameterHash = Animator.StringToHash(basicAttackTriggerParameterName);

        HorizontalSlashParameterHash = Animator.StringToHash(horizontalSlashParameterName);
        HollowRedParamaterHash = Animator.StringToHash(hollowRedParameterName);
        LaserRainParameterHash = Animator.StringToHash(laserRainParameterName);
        MeteorShowerParameterHash = Animator.StringToHash(meteorShowerParameterName);
        RedEnergyExplosionParameterHash = Animator.StringToHash(redEnergyExplosionParameterName);
    }

}
