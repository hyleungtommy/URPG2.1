using System.Collections.Generic;

public class SkillBuff : Skill{
    public override UseOn UseOn => (Type == SkillType.BuffSelf) ? UseOn.Self : UseOn.Target;
    public SkillBuff(SkillTemplate template) : base(template){

    }

    public override void Use(BattleEntity user, List<BattleEntity> targets)
    {
        base.Use(user, targets);
        ApplyBuffs(user, targets);
    }
}