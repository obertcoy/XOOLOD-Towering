using System;
using UnityEngine;

public class EnemyStateReusableData
{
    #region Movement
    public float MovementSpeedModifier { get; set; } = 1f;

    public Transform NearestPlayer;

    public IDamageable LatestSource;

    private float currentTakeDamageAnimationCooldown = 0f;
    public float CurrentTakeDamageAnimationCooldown
    {
        get { return currentTakeDamageAnimationCooldown; }
        set { currentTakeDamageAnimationCooldown = Mathf.Max(value, 0f); }
    }


    private float currentAttackIntervalCooldown = 0f;
    public float CurrentAttackIntervalCooldown
    {
        get { return currentAttackIntervalCooldown; }
        set { currentAttackIntervalCooldown = Mathf.Max(value, 0f); }
    }

    private float currentChaseCooldown = 0f;
    public float CurrentChaseCooldown
    {
        get { return currentChaseCooldown; }
        set { currentChaseCooldown = Mathf.Max(value, 0f); }
    }

    private float currentPatrolCooldown = 0f;
    public float CurrentPatrolCooldown
    {
        get { return currentPatrolCooldown; }
        set { currentPatrolCooldown = Mathf.Max(value, 0f); }
    }

    private float currentAttack1Cooldown = 0f;
    public float CurrentAttack1Cooldown
    {
        get { return currentAttack1Cooldown; }
        set { currentAttack1Cooldown = Mathf.Max(value, 0f); }
    }

    private float currentAttack2Cooldown = 0f;
    public float CurrentAttack2Cooldown
    {
        get { return currentAttack2Cooldown; }
        set { currentAttack2Cooldown = Mathf.Max(value, 0f); }
    }

    private float currentAttack3Cooldown = 0f;
    public float CurrentAttack3Cooldown
    {
        get { return currentAttack3Cooldown; }
        set { currentAttack3Cooldown = Mathf.Max(value, 0f); }
    }

    private float currentAttack4Cooldown = 0f;
    public float CurrentAttack4Cooldown
    {
        get { return currentAttack4Cooldown; }
        set { currentAttack4Cooldown = Mathf.Max(value, 0f); }
    }

    public int CurrentWaypointIndex = 0;

    public ColliderDamageDealer[] ColliderDamageDealers;
    public ActiveAbilityDamageDealer ActiveAbilityDamageDealer;
    public WeaponDamageDealer WeaponDamageDealer;


    public float GetCurrentAttackCooldown(int idx)
    {
        if (idx == 0) return currentAttack1Cooldown;
        if (idx == 1) return currentAttack2Cooldown;
        if (idx == 2) return currentAttack3Cooldown;
        if (idx == 3) return currentAttack4Cooldown;

        return 999;
    }

    public bool isAirborne;
    public IState NextDesiredState;

    #endregion
}
