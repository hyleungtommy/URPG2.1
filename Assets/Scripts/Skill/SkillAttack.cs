using System.Collections.Generic;
using UnityEngine;

public class SkillAttack : Skill
{
    public SkillAttack(SkillTemplate template) : base(template)
    {
    }

    public override void Use(BattleEntity user, List<BattleEntity> targets)
    {
        base.Use(user, targets);
        foreach (BattleEntity target in targets)
        {
            for (int i = 0; i < HitCount; i++)
            {
                int rawDamage = Mathf.Max(1, user.Stats.ATK - target.Stats.DEF);
                int damage = (int)(rawDamage * Modifier);
                target.OnReceiveDamage(damage);
            }

        }
    }
}

