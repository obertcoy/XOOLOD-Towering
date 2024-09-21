using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    protected bool canDealDamage;
    protected List<GameObject> hasDealtDamage;
    protected float damage;
    protected IDamageable source;
    [field: SerializeField] public string LayerName { get; set; }

    protected virtual void Awake()
    {
        source = GetComponentInParent<IDamageable>();
    }

    protected virtual void Start()
    {
        canDealDamage = false;
        hasDealtDamage = new List<GameObject>();
    }

    public virtual void StartDealDamage()
    {

        canDealDamage = true;
        hasDealtDamage.Clear();
    }

    public virtual void EndDealDamage()
    {
        canDealDamage = false;
    }

    protected virtual IEnumerator DealDamage(GameObject gameObject)
    {
        if (gameObject != null && gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(source, this.damage);

            yield return new WaitForSeconds(0.15f);
        }

        yield break;
    }

    public virtual void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public virtual void SetSource(IDamageable source)
    {
        this.source = source;
    }

 
}
