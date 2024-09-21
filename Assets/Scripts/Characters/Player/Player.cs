using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerCheatManager))]

public class Player : MonoBehaviour, IDamageable
{

    private static Player _instance;
    public static Player Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Player>();

            }
            return _instance;
        }
    }

    [field: Header("References")]
    [field: SerializeField] public PlayerSO Data { get; private set; }
    [field: SerializeField] public PlayerSpawnManager SpawnManager { get; private set; }
    [field: SerializeField] public PlayerLayerData LayerData { get; private set; }
    [field: SerializeField] public PlayerStats Stats { get; set; }
    [field: SerializeField] public PlayerAbilitySystem AbilitySystem { get; set; }

    [field: Header("Collisions")]
    [field: SerializeField] public PlayerCapsuleColliderUtility ColliderUtility { get; private set; }

    [field: Header("Cameras")]
    [field: SerializeField] public PlayerCameraUtility CameraUtility { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }


    public Transform MainCameraTransform { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInput Input { get; private set; }
    public PlayerLockSystem LockSystem { get; private set; }


    private PlayerStateMachine playerStateMachine;
    private PlayerCheatManager playerCheatManager;
    
    private void Awake()
    {

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerInput>();
        LockSystem = GetComponent<PlayerLockSystem>();
        playerCheatManager = GetComponent<PlayerCheatManager>();

        Stats.Initialize(this);
        AbilitySystem.Initialize(this);
        ColliderUtility.Initialize(gameObject);
        ColliderUtility.CalculateCapsuleColliderDimensions();
        CameraUtility.Initialize();
        AnimationData.Initialize();

        playerCheatManager.Initialize(this);
        LockSystem.Initialize(transform);

        MainCameraTransform = Camera.main.transform;

        playerStateMachine = new PlayerStateMachine(this);
        _instance = this;


    }

    private void OnValidate()
    {
        ColliderUtility.Initialize(gameObject);
        ColliderUtility.CalculateCapsuleColliderDimensions();
    }

    private void Start()
    {
        InitializeWeapon();

        playerStateMachine.ChangeState(playerStateMachine.IdlingState);

    }

    private void Update()
    {
        playerStateMachine.HandleInput();
        playerStateMachine.Update();

        Stats.Update();
    }
    private void FixedUpdate()
    {
        playerStateMachine.PhysicsUpdate();
    }
    private void OnTriggerEnter(Collider collider)
    {
        playerStateMachine.OnTriggerEnter(collider);
    }

    private void OnTriggerExit(Collider collider)
    {
        playerStateMachine.OnTriggerExit(collider);
    }

    public void OnMovementStateAnimationEnterEvent()
    {
        playerStateMachine.OnAnimationEnterEvent();
    }
    public void OnMovementStateAnimationExitEvent()
    {
        playerStateMachine.OnAnimationExitEvent();
    }
    public void OnMovementStateAnimationTransitionEvent()
    {
        playerStateMachine.OnAnimationTransitionEvent();
    }

    #region IDamageable Methods

    public void StartDealDamage()
    {
        playerStateMachine.StartDealDamage();
    }

    public void EndDealDamage()
    {
        playerStateMachine.EndDealDamage();
    }
    public void TakeDamage(IDamageable source, float damage)
    {

        Stats.TakeDamage(damage * Stats.CheatData.TakeDamageModifier);

        PopupText.Create(transform.position, gameObject, ((int)damage).ToString(), PopupTextType.Damage, Color.red);

        playerStateMachine.TakeDamage(source, damage);
    }

    public void GetDropLoot(float exp, int gold)
    {

        Stats.RuntimeData.CurrentExp += exp;
        Stats.RuntimeData.CurrentGold += gold;

        PopupText.Create(transform.position, gameObject, string.Format("+{0} exp", (int)exp), PopupTextType.Drop, Color.cyan);
        PopupText.Create(transform.position, gameObject, string.Format("+{0} gold", (int)gold), PopupTextType.Drop);

        while (true) {


            if (!Stats.CheckLevelUp()) break;

            LevelUpEffect.Create(transform.position, gameObject);

            PopupText.Create(transform.position, gameObject, "LEVEL UP!", PopupTextType.LevelUp);

            StartCoroutine(Stats.WaitAnimation());

        }

    }

 

    #endregion


    public void UnlockAbility(PlayerAbilitySO ability)
    {
        if (Stats.RuntimeData.CurrentGold < ability.UnlockData.Gold) return;
        
        Stats.RuntimeData.CurrentGold -= ability.UnlockData.Gold;
        
        AbilitySystem.UnlockAbility(playerStateMachine, ability);
        
    }

    public void ChangeActiveAbility(PlayerActiveAbilitiesControlEnum control, PlayerActiveAbilitySO activeAbility)
    {
        if (Stats.RuntimeData.CurrentGold < activeAbility.UnlockData.Gold) return;

        Stats.RuntimeData.CurrentGold -= activeAbility.UnlockData.Gold;

        AbilitySystem.ChangeActiveAbility(control, activeAbility);
    }

    public GameObject InstantiateGameObject(GameObject gameObject, Transform transform = null, Vector3 position = default, Quaternion rotation = default)
    {
        if (transform == null) return Instantiate(gameObject, position, rotation);

        return Instantiate(gameObject, transform);
    }

    public void DestroyObject(GameObject obj, float seconds = -1f)
    {

        if (gameObject != null && seconds == -1)
        {
            Destroy(obj);
        } else if(seconds != -1)
        {
            Destroy(obj, seconds);
        }
        
    }

    public void ResetPlayer()
    {
        if (playerStateMachine.ReusableData.CurrentWeaponInHand != null) Destroy(playerStateMachine.ReusableData.CurrentWeaponInHand);

        playerStateMachine.ReusableData.CombatToggle = false;
        playerStateMachine.ReusableData.CurrentWeaponInHand = null;

        InitializeWeapon();

        Stats.Initialize(this);

        playerStateMachine.ChangeState(playerStateMachine.IdlingState);
    }

    private void InitializeWeapon()
    {

        playerStateMachine.ReusableData.CurrentWeaponInHand = InstantiateGameObject(
         GameAssets.Instance.Sword,
         Data.CombatData.SwordData.SheathHolder.transform
         );

        playerStateMachine.ReusableData.CurrentWeaponInHand.GetComponentInChildren<TrailRenderer>().emitting = false;
    }

}
