using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStats
{
    [field: Header("Stats")]
    [field: SerializeField] public PlayerDefaultStatsData DefaultData { get; private set; }
    [field: SerializeField] public PlayerRuntimeStatsData RuntimeData { get; set; }
    [field: SerializeField] public PlayerPassiveModifierData PassiveModifier { get; set; }
    [field: SerializeField] public PlayerLevelModiferData LevelModifierData { get; private set; }
    [field: SerializeField] public PlayerCheatData CheatData { get; set; }

    [field: Header("UI")]

    [field: SerializeField] public PlayerStatsMenuUI StatsMenuUI { get; set; }
    [field: SerializeField] private PlayerRuntimeStatsUI RuntimeStatsUI { get; set; }

    private bool isDead;

    #region Main Methods

    public void Initialize(Player player)
    {
        isDead = false;

        RuntimeData.Initialize(DefaultData.MaxHealth);

        RuntimeStatsUI.Initialize(RuntimeData.Health, DefaultData.MaxHealth, RuntimeData.CurrentExp, DefaultData.ExpPerLevel, RuntimeData.Level);

        player.StartCoroutine(HealthRegenCoroutine());
    }


    public void Update()
    {

        RuntimeData.HealthRegenCooldown -= Time.deltaTime;

    }

    public void TakeDamage(float damage)
    {
        RuntimeData.Health -= damage;

        RuntimeData.HealthRegenCooldown = DefaultData.HealthRegenCooldown;

        RuntimeStatsUI.UpdateHealth(RuntimeData.Health, DefaultData.MaxHealth);

        RuntimeData.HealthRegenCooldown = DefaultData.HealthRegenCooldown;

        if(RuntimeData.Health <= 0)
        {
            isDead = true;
        }
    }

    public bool CheckLevelUp()
    {

        RuntimeStatsUI.UpdateExperience(RuntimeData.CurrentExp, DefaultData.ExpPerLevel, RuntimeData.Level);

        if (RuntimeData.CurrentExp >= DefaultData.ExpPerLevel && RuntimeData.Level < DefaultData.MaxLevel)
        {
            
            LevelUp();


            return true;
        }

        return false;
    }

    public void OpenStatsMenuUI()
    {
        StatsMenuUI.Display(RuntimeData.Level, DefaultData.MaxHealth, DefaultData.AttackDamage, RuntimeData.CurrentGold);
    }

    private IEnumerator HealthRegenCoroutine()
    {

        while (!isDead && RuntimeData.Health > 0)
        {
            yield return new WaitForSeconds(1f);

            if (RuntimeData.Health >= DefaultData.MaxHealth || RuntimeData.HealthRegenCooldown > 0) continue;

            float healthRegen = (DefaultData.MaxHealth / 100) * PassiveModifier.HealthRegenModifier;

            RuntimeData.Health += healthRegen;

            RuntimeStatsUI.UpdateHealth(RuntimeData.Health, DefaultData.MaxHealth);

        }

    }

    public IEnumerator WaitAnimation()
    {
    
         yield return new WaitForSeconds(2f);

    }

    private void LevelUp()
    {

        RuntimeData.LevelUp(DefaultData.ExpPerLevel);
        
        RuntimeData.Health = DefaultData.MaxHealth;
        RuntimeData.CurrentAttackDamage = DefaultData.AttackDamage;

        DefaultData.LevelUp(LevelModifierData.HealthModifier, LevelModifierData.AttackModifier);

        RuntimeStatsUI.UpdateExperience(RuntimeData.CurrentExp, DefaultData.ExpPerLevel, RuntimeData.Level);
        RuntimeStatsUI.UpdateHealth(RuntimeData.Health, DefaultData.MaxHealth);


    }
  

    #endregion

}
