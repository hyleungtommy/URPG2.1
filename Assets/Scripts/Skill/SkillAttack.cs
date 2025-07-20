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
                user.PerformSkillAttack(target, this);
            }

        }
    }
}

