using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void StartDealDamage();
    void EndDealDamage();
    void TakeDamage(IDamageable source, float damage);
    void DestroyObject(GameObject obj, float seconds = -1f);

    void GetDropLoot(float exp, int gold);
}