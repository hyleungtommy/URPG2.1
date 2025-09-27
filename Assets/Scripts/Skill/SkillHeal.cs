using System.Collections.Generic;
using UnityEngine;

public class SkillHeal : Skill
{
    public override UseOn UseOn => UseOn.Target;
    
    public SkillHeal(SkillTemplate template) : base(template)
    {
    }

    public override void Use(BattleEntity user, List<BattleEntity> targets)
    {
        base.Use(user, targets);
        
        foreach (BattleEntity target in targets)
        {
            // Calculate heal amount: Modifier * MATK
            int healAmount = Mathf.RoundToInt(user.Stats.MATK * Modifier);
            
            // Apply healing
            int originalHP = target.CurrentHP;
            target.Heal(healAmount);
            int actualHeal = target.CurrentHP - originalHP;
            
            if (actualHeal > 0)
            {
                Debug.Log($"{user.Name} heals {target.Name} for {actualHeal} HP!");
                
                // Show floating heal number
                if (BattleScene.Instance != null)
                {
                    BattleScene.Instance.ShowFloatingHeal(actualHeal, target);
                }
            }
        }
        
        // Apply any buffs from the skill
        ApplyBuffs(user, targets);
    }
}