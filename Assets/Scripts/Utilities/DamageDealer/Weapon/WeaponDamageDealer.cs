using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WeaponDamageDealer : DamageDealer
{

    [field: SerializeField] public float weaponLength;
    [field: SerializeField] public VisualEffect SlashVFX;

    private void Update()
    {
        if (canDealDamage)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, LayerMask.GetMask(LayerName)))
            { 
                if (!hasDealtDamage.Contains(hit.transform.gameObject))
                {
                    hasDealtDamage.Add(hit.transform.gameObject);
                    StartCoroutine(DealDamage(hit.transform.gameObject));
                }

            }

            Debug.DrawRay(transform.position, -transform.up * weaponLength, Color.red);

        }
    }

    public override void StartDealDamage()
    {
        canDealDamage = true;
        hasDealtDamage.Clear();

        //SlashVFX.gameObject.SetActive(true);
        //SlashVFX.SetBool("UseForce", true);
    }

    public override void EndDealDamage()
    {
        canDealDamage = false;

        //Invoke(nameof(DeactivateSlashVFX), 0.2f);
        //SlashVFX.SetBool("UseForce", false);

    }

    private void DeactivateSlashVFX()
    {
        // SlashVFX.gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * weaponLength);
    }

}
