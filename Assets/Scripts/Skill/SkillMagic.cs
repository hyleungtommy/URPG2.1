using System.Collections.Generic;
using UnityEngine;

public class SkillMagic : Skill
{
    public override UseOn UseOn => UseOn.Target;
    public SkillMagic(SkillTemplate template) : base(template)
    {
    }

    public override void Use(BattleEntity user, List<BattleEntity> targets)
    {
        base.Use(user, targets);
        foreach (BattleEntity target in targets)
        {
            for (int i = 0; i < HitCount; i++)
            {
                user.PerformSkillMagic(target, this);
            }
        }
        // Apply buffs once after all damage has been dealt
        ApplyBuffs(user, targets);
    }
}