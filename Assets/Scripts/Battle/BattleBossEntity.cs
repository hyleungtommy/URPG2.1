using UnityEngine;

public class BattleBossEntity : BattleEnemyEntity
{
    public int MaxShield { get; private set; }
    public int CurrentShield { get; private set; }
    public bool IsShieldActive => CurrentShield > 0;
    public float DamageMultiplier { get; set; }
    private int shieldRegenerateCooldown = 5;
    public BattleBossEntity(EnemyTemplate enemyTemplate, BattleManager manager) : base(enemyTemplate, manager)
    {
        MaxShield = enemyTemplate.HP/2;
        CurrentShield = MaxShield;
        DamageMultiplier = 1;
    }

    public override void OnReceiveDamage(int amount, BattleEntity attacker = null)
    {
        base.OnReceiveDamage((int)(amount * DamageMultiplier), attacker);
        if (IsShieldActive)
        {
            CurrentShield -= amount;
            DamageMultiplier += (float)amount/(float)base.Stats.HP;
        }else{
            DamageMultiplier += (float)amount/(float)base.Stats.HP * 10;
        }
    }

    public override void OnEndTurn()
    {
        base.OnEndTurn();
        
        if(IsShieldActive){
            // at the end of each turn, the boss will regenerate 5% of its max shield
            CurrentShield += MaxShield/20;
            if (CurrentShield > MaxShield)
            {
                CurrentShield = MaxShield;
            }
        }else{
            // if shield has broken, the boss will regenerate its shield after a cooldown
            shieldRegenerateCooldown--;
            if (shieldRegenerateCooldown <= 0)
            {
                DamageMultiplier = 1;
                CurrentShield = MaxShield;
                shieldRegenerateCooldown = 5;
            }
        }
    }
    
}