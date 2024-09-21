using System;
using UnityEngine;

[Serializable]
public class EnemyAnimationData
{
    [field: Header("State Group Paramater Names")]

    [field: SerializeField] private string groundedParameterName = "Grounded";
    [field: SerializeField] private string airborneParameterName = "Airborne";
    [field: SerializeField] private string movingParameterName = "Moving";
    [field: SerializeField] private string combatParameterName = "Combat";

    [Header("Grounded Parameter Names")]

    [field: SerializeField] private string walkingParameterName = "isWalking";
    [field: SerializeField] private string runningParameterName = "isRunning";
    [field: SerializeField] private string landingParameterName = "isLanding";

    [field: Header("Airborne Parameter Names")]

    [field: SerializeField] private string takingOffParameterName = "isTakingOff";
    [field: SerializeField] private string flyingParameterName = "isFlying";


    [field: Header("Combat Paramater Names")]

    [field: SerializeField] private string takingDamageParameterName = "isTakingDamage";
    [field: SerializeField] private string deadParameterName = "isDead";

    [field: SerializeField] private string attacking1ParameterName = "isAttacking1";
    [field: SerializeField] private string attacking2ParameterName = "isAttacking2";
    [field: SerializeField] private string attacking3ParameterName = "isAttacking3";
    [field: SerializeField] private string attacking4ParameterName = "isAttacking4";

    public int GroundedParameterHash { get; private set; }
    public int AirborneParameterHash { get; private set; }
    public int MovingParameterHash { get; private set; }
    public int CombatParameterHash { get; private set; }
    public int WalkingParameterHash { get; private set; }
    public int RunningParameterHash { get; private set; }

    public int TakingOffParameterHash { get; private set; }
    public int LandingParameterHash { get; private set; }
    public int FlyingParameterHash { get; private set; }

    public int TakingDamageParameterHash { get; private set; }
    public int DeadParameterHash { get; private set; }
    public int Attacking1ParameterHash { get; private set; }
    public int Attacking2ParameterHash { get; private set; }
    public int Attacking3ParameterHash { get; private set; }
    public int Attacking4ParameterHash { get; private set; }


    public void Initialize()
    {
        GroundedParameterHash = Animator.StringToHash(groundedParameterName);
        AirborneParameterHash = Animator.StringToHash(airborneParameterName);

        MovingParameterHash = Animator.StringToHash(movingParameterName);
        CombatParameterHash = Animator.StringToHash(combatParameterName);

        WalkingParameterHash = Animator.StringToHash(walkingParameterName);
        RunningParameterHash = Animator.StringToHash(runningParameterName);
        LandingParameterHash = Animator.StringToHash(landingParameterName);

        TakingOffParameterHash = Animator.StringToHash(takingOffParameterName);
        FlyingParameterHash = Animator.StringToHash(flyingParameterName);

        TakingDamageParameterHash = Animator.StringToHash(takingDamageParameterName);
        DeadParameterHash = Animator.StringToHash(deadParameterName);

        Attacking1ParameterHash = Animator.StringToHash(attacking1ParameterName);
        Attacking2ParameterHash = Animator.StringToHash(attacking2ParameterName);
        Attacking3ParameterHash = Animator.StringToHash(attacking3ParameterName);
        Attacking4ParameterHash = Animator.StringToHash(attacking4ParameterName);


    }

}
    
