using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAbilityDamageDealer : DamageDealer
{

    private Vector3 initialPosition;

    private Collider abilityCollider;
    
    [field: SerializeField] public PlayerActiveAbilitySO PlayerData { get; private set; }
    [field: SerializeField] public EnemyActiveAbilitySO EnemyData { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        abilityCollider = gameObject.GetComponentInChildren<Collider>();
    }

    protected override void Start()
    {
        base.Start();
        initialPosition = transform.parent.position;

        abilityCollider.enabled = false;
    }

    private void Update()
    {
        if (PlayerData != null && PlayerData.Moving)
        {
            MoveForward();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        // Debug.Log("Deal Damage: " + canDealDamage + ", Collide: " + collider.gameObject  + " , count: " + count);

        if (canDealDamage)
        {

            if (!hasDealtDamage.Contains(collider.gameObject) && collider.gameObject.layer.Equals(LayerMask.NameToLayer(LayerName)))
            {
                hasDealtDamage.Add(collider.gameObject);
                StartCoroutine(DealDamage(collider.gameObject));
            }
        }


    }


    private void MoveForward()
    {

        transform.parent.Translate(Vector3.forward * PlayerData.MovingSpeed * Time.deltaTime);

        float distanceToParent = Vector3.Distance(transform.position, initialPosition);

        if (distanceToParent >= PlayerData.MaxDistance)
        {
            Destroy(transform.parent.gameObject);
        }

    }

    protected override IEnumerator DealDamage(GameObject gameObject)
    {

        if (PlayerData != null)
        {
            for (int i = 0; i < PlayerData.AbilityData.HitCount; i++)
            {
                if (gameObject != null && gameObject.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(source, damage);
                    yield return new WaitForSeconds(0.15f);
                }
            }
        }
        else if (EnemyData != null)
        {
            for (int i = 0; i < EnemyData.AbilityData.HitCount; i++)
            {
                if (gameObject != null && gameObject.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(source, damage);
                    yield return new WaitForSeconds(0.15f);
                }
            }
        }


    }

    public override void StartDealDamage()
    {
        abilityCollider.enabled = true;

        base.StartDealDamage();
    }

    public override void SetDamage(float damage)
    {
        if (PlayerData != null && PlayerData != null)
        {
            this.damage = damage * PlayerData.AbilityData.DamageMultiplier;
        } else if(EnemyData != null && EnemyData != null)
        {
            this.damage = damage * EnemyData.AbilityData.DamageMultiplier;
        }
    }
}
