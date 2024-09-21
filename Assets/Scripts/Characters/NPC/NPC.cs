using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum NPCTypeEnum
{
    AbilityShop,
    ChangeAbilityShop,
    ChallengePhoebusNPC
}

public class NPC : MonoBehaviour, IInteractable
{

    [field: Header("References")]

    [field: SerializeField] public NPCSO Data { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }

    protected NPCReusableData ReusableData { get; set; }

    private NPCStateMachine npcStateMachine;

    private int playerLayerMask;

    private void Awake()
    {

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();

        ReusableData = new NPCReusableData();

        playerLayerMask = 1 << LayerMask.NameToLayer("Player");

        npcStateMachine = new NPCStateMachine(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {

        CheckPlayerNearby();

    }

    private void LateUpdate()
    {

        LookAtPlayer();

    }

    public void Interact(Player player)
    {

        if (Data.Type.Equals(NPCTypeEnum.AbilityShop)) AbilityShop.Create(player);
        if (Data.Type.Equals(NPCTypeEnum.ChangeAbilityShop)) ChangeAbilityShop.Create(player);
        if (Data.Type.Equals(NPCTypeEnum.ChallengePhoebusNPC)) BossNPCInteract(player);

    }
    #region Main Methods

    private void FindNearestPlayer(Collider[] colliders)
    {
        float nearestDistance = float.MaxValue;
        ReusableData.NearestPlayer = null;


        foreach (Collider collider in colliders)
        {
            Transform playerTransform = collider.transform;

            float playerDistance = Vector3.Distance(transform.position, playerTransform.position);

            if (playerDistance < nearestDistance)
            {
                nearestDistance = playerDistance;

                ReusableData.NearestPlayer = playerTransform;
            }
        }

    }

    private void CheckPlayerNearby()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, Data.LookRange, playerLayerMask);

        if (colliders.Length > 0)
        {

            FindNearestPlayer(colliders);

        }
        else
        {
            ReusableData.NearestPlayer = null;
        }
    }

    protected void LookAtPlayer()
    {

        if (ReusableData.NearestPlayer == null) return;

        Vector3 direction = (ReusableData.NearestPlayer.position - transform.position).normalized;

        Quaternion rotationGoal = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotationGoal, Data.RotationSpeed);

    }

    #endregion

    #region NPC Methods

    private void BossNPCInteract(Player player)
    {
        AreaWarning.Create(player, "Phoebus's Chamber", 60, 80, () => player.SpawnManager.SpawnAtBossScene(player));
    }

    #endregion
}
