using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }
    public PlayerStateReusableData ReusableData { get; }

    #region Movement State

    public PlayerIdlingState IdlingState { get; }
    public PlayerWalkingState WalkingState { get; }
    public PlayerRunningState RunningState { get; }
    public PlayerDashingState DashingState { get; }

    public PlayerLightStoppingState LightStoppingState { get; }
    public PlayerMediumStoppingState MediumStoppingState { get; }
    public PlayerHardStoppingState HardStoppingState { get; }

    public PlayerJumpingState JumpingState { get; }
    public PlayerFallingState FallingState { get; }

    public PlayerLightLandingState LightLandingState { get; }
    public PlayerHardLandingState HardLandingState { get; }
    public PlayerRollingState RollingState { get; }

    public PlayerDeadState DeadState { get; }

    #endregion

    #region Combat State

    public PlayerDrawingSwordState DrawSwordState { get; }
    public PlayerSheatingSwordState SheateSwordState { get; }
    public PlayerBasicAttackState BasicAttackState { get; }

    #endregion

    #region Ability Casting State

    public PlayerCastingState CastingState;

    public PlayerCastingHorizontalSlashState CastingHorizontalState;
    public PlayerCastingHollowRedState CastingHollowRedState;
    public PlayerCastingLaserRainState CastingLaserRainState;
    public PlayerCastingMeteorShowerState CastingMeteorShowerState;
    public PlayerCastingRedEnergyExplosionState RedEnergyExplosionState;

    #endregion

    public PlayerStateMachine(Player player)
    {
        Player = player;
        ReusableData = new PlayerStateReusableData();

        IdlingState = new PlayerIdlingState(this);
        WalkingState = new PlayerWalkingState(this);
        RunningState = new PlayerRunningState(this);
        DashingState = new PlayerDashingState(this);

        LightStoppingState = new PlayerLightStoppingState(this);
        MediumStoppingState = new PlayerMediumStoppingState(this);
        HardStoppingState = new PlayerHardStoppingState(this);

        JumpingState = new PlayerJumpingState(this);
        FallingState = new PlayerFallingState(this);

        DeadState = new PlayerDeadState(this);

        LightLandingState = new PlayerLightLandingState(this);
        HardLandingState = new PlayerHardLandingState(this);
        RollingState = new PlayerRollingState(this);
  

        DrawSwordState = new PlayerDrawingSwordState(this);
        SheateSwordState = new PlayerSheatingSwordState(this);

        BasicAttackState = new PlayerBasicAttackState(this);

        CastingState = new PlayerCastingState(this);

        CastingHorizontalState = new PlayerCastingHorizontalSlashState(this);
        CastingHollowRedState = new PlayerCastingHollowRedState(this);
        CastingLaserRainState = new PlayerCastingLaserRainState(this);
        CastingMeteorShowerState = new PlayerCastingMeteorShowerState(this);
        RedEnergyExplosionState = new PlayerCastingRedEnergyExplosionState(this);

    }

 


}
