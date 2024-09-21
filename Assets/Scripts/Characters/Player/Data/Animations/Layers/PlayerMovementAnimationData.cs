using System;
using UnityEngine;

[Serializable]
public class PlayerMovementAnimationData
{

    [Header("State Group Paramater Names")]

    [field: SerializeField] private String groundedParameterName = "Grounded";
    [field: SerializeField] private String movingParameterName = "Moving";
    [field: SerializeField] private String stoppingParameterName = "Stopping";
    [field: SerializeField] private String landingParameterName = "Landing";
    [field: SerializeField] private String airborneParameterName = "Airborne";

    [Header("Grounded Parameter Names")]

    [field: SerializeField] private String idleParameterName = "isIdling";
    [field: SerializeField] private String dashParameterName = "isDashing";
    [field: SerializeField] private String walkParameterName = "isWalking";
    [field: SerializeField] private String runParameterName = "isRunning";
    [field: SerializeField] private String mediumStopParameterName = "isMediumStopping";
    [field: SerializeField] private String hardStopParameterName = "isHardStopping";
    [field: SerializeField] private String rollParameterName = "isRolling";
    [field: SerializeField] private String hardLandParameterName = "isHardLanding";
    [field: SerializeField] private String deadParameterName = "isDead";


    [Header("Airborne Parameter Names")]

    [field: SerializeField] private String fallParameterName = "isFalling";

    public int GroundedParameterHash { get; private set; }
    public int MovingParameterHash { get; private set; }
    public int StoppingParameterHash { get; private set; }
    public int LandingParameterHash { get; private set; }
    public int AirborneParameterHash { get; private set; }

    public int IdleParameterHash { get; private set; }
    public int DashParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }
    public int MediumStopParameterHash { get; private set; }
    public int HardStopParameterHash { get; private set; }
    public int RollParameterHash { get; private set; }
    public int HardLandParameterHash { get; private set; }
    public int FallParameterHash { get; private set; }
    public int DeadParameterHash { get; private set; }

    public void Initialize()
    {

        GroundedParameterHash = Animator.StringToHash(groundedParameterName);
        MovingParameterHash = Animator.StringToHash(movingParameterName);
        StoppingParameterHash = Animator.StringToHash(stoppingParameterName);
        LandingParameterHash = Animator.StringToHash(landingParameterName);
        AirborneParameterHash = Animator.StringToHash(airborneParameterName);

        IdleParameterHash = Animator.StringToHash(idleParameterName);
        DashParameterHash = Animator.StringToHash(dashParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);
        RunParameterHash = Animator.StringToHash(runParameterName);
        MediumStopParameterHash = Animator.StringToHash(mediumStopParameterName);
        HardStopParameterHash = Animator.StringToHash(hardStopParameterName);
        RollParameterHash = Animator.StringToHash(rollParameterName);
        HardLandParameterHash = Animator.StringToHash(hardLandParameterName);

        FallParameterHash = Animator.StringToHash(fallParameterName);

        DeadParameterHash = Animator.StringToHash(deadParameterName);

    }
}
