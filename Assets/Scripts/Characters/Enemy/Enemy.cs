using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum EnemyNameEnum
{
    Rhino,
    Minotaur,
    EarthElemental,
    FireElemental,
    IceElemental,
    Phoebus,
    Minions,
    
}

public class Enemy : MonoBehaviour, IDamageable
{
    
    [field: Header("References")]

    [field: SerializeField] public EnemySO Data { get; private set; }
    [field: SerializeField] public EnemyStats Stats { get; set; }

    [field: SerializeField] public EnemyAbilityList AbilityList { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public EnemyAnimationData AnimationData { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }

    public List<DamageDealer> DamageDealerList { get; private set; }

    private EnemyStateMachine enemyStateMachine;

    private EnemyHealthbar healthbar;

    public EnemySpawner Spawner { get; private set; }


    private void Awake()
    {

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();

        DamageDealerList = new List<DamageDealer>();
        GetAllDamageDealer();

        Stats.Initialize();
        AnimationData.Initialize();
       
        healthbar = GetComponent<EnemyHealthbar>();


        enemyStateMachine = new EnemyStateMachine(this);
    }

    private void Start()
    {

        enemyStateMachine.ChangeState(enemyStateMachine.IdlingState);

    }
    private void Update()
    {
        enemyStateMachine.Update();
    }
    private void FixedUpdate()
    {
        enemyStateMachine.PhysicsUpdate();
    }
    private void OnTriggerEnter(Collider collider)
    {
        enemyStateMachine.OnTriggerEnter(collider);
    }

    private void OnTriggerExit(Collider collider)
    {
        enemyStateMachine.OnTriggerExit(collider);
    }

    public void OnMovementStateAnimationEnterEvent()
    {
        enemyStateMachine.OnAnimationEnterEvent();
    }
    public void OnMovementStateAnimationExitEvent()
    {
        enemyStateMachine.OnAnimationExitEvent();
    }
    public void OnMovementStateAnimationTransitionEvent()
    {
        enemyStateMachine.OnAnimationTransitionEvent();
    }


    #region IDamageable Methods

    public void StartDealDamage()
    {
        enemyStateMachine.StartDealDamage();
    }

    public void EndDealDamage()
    {
        enemyStateMachine.EndDealDamage();
    }

    public void TakeDamage(IDamageable source, float damage)
    {

        enemyStateMachine.ReusableData.LatestSource = source;

        if (Stats.RuntimeData.Health > 0)
        {
            Stats.RuntimeData.Health -= damage;

            healthbar.UpdateHealth();

            PopupText.Create(transform.position, gameObject, ((int) damage).ToString(), PopupTextType.Damage);

            HitEffect.Create(transform.position, gameObject);

            enemyStateMachine.TakeDamage(source, damage);
        }


    }

    public void GetDropLoot(float exp, int gold)
    {
    }

    public void DestroyObject(GameObject obj, float seconds = -1)
    {
        Destroy(obj);
    }
    #endregion

    #region Main Methods

    private void GetAllDamageDealer()
    {

        foreach (Transform child in transform)
        {

            DamageDealerList = new List<DamageDealer>(GetComponentsInChildren<DamageDealer>());

            foreach (DamageDealer damageDealer in DamageDealerList)
            {
                Debug.Log("Damage dealer: " + damageDealer);
            }
        }
    }

    public GameObject InstantiateGameObject(GameObject gameObject, Transform transform = null, Vector3 position = default, Quaternion rotation = default)
    {
        if (transform == null) return Instantiate(gameObject, position, rotation);

        return Instantiate(gameObject, transform);
    }
    
    public void SpawnerBy(EnemySpawner spawner)
    {
        this.Spawner = spawner;
    }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Data.CombatData.AggroRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, Data.CombatData.FlyingAggroRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Data.GroundedData.WalkData.PatrolRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Data.CombatData.AttackData[0].AttackRange);

    }

  
}
