using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDamageDealer : DamageDealer
{
  
    protected override void Start()
    {
        base.Start();
    }

    private void OnTriggerStay(Collider collider)
    {

        if (canDealDamage)
        {
            if (!hasDealtDamage.Contains(collider.gameObject) && collider.gameObject.layer.Equals(LayerMask.NameToLayer(LayerName)))
            {
                hasDealtDamage.Add(collider.gameObject);
                StartCoroutine(DealDamage(collider.gameObject));
            }

        }
    }


}
