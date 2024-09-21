using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
 
    public Enemy Enemy { get; }
    public EnemyStateReusableData ReusableData { get; set; }

    #region Movement

    public EnemyIdlingState IdlingState { get; private set; }
    public EnemyWalkingState WalkingState { get; private set; }
    public EnemyRunningState RunningState { get; private set; }

    public EnemyTakingOffState TakingOffState { get; private set; }
    public EnemyLandingState LandingState { get; private set; }
    public EnemyFlyingState FlyingState { get; private set; }

    #endregion

    #region Combat

    public EnemyAttacking1State Attacking1State { get; private set; }
    public EnemyAttacking2State Attacking2State { get; private set; }
    public EnemyAttacking3State Attacking3State { get; private set; }
    public EnemyAttacking4State Attacking4State { get; private set; }


    #endregion

    #region Damageable

    public EnemyTakingDamageState TakingDamageState { get; private set; }
    public EnemyDeadState DeadState { get; private set; }

    #endregion

    public EnemyStateMachine(Enemy enemy)
    {
        Enemy = enemy;
        ReusableData = new EnemyStateReusableData();

        IdlingState = new EnemyIdlingState(this);
        WalkingState = new EnemyWalkingState(this);
        RunningState = new EnemyRunningState(this);

        TakingOffState = new EnemyTakingOffState(this);
        LandingState = new EnemyLandingState(this);
        FlyingState = new EnemyFlyingState(this);

        Attacking1State = new EnemyAttacking1State(this);
        Attacking2State = new EnemyAttacking2State(this);
        Attacking3State = new EnemyAttacking3State(this);
        Attacking4State = new EnemyAttacking4State(this);


        TakingDamageState = new EnemyTakingDamageState(this);
        DeadState = new EnemyDeadState(this);
    }

}
