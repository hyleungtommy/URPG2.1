using System.Collections.Generic;
using UnityEngine;

public abstract class Skill
{
    public int Id { get; private set; }
    public Sprite Icon { get; private set; }
    public string Name { get; private set; }
    public SkillType Type { get; private set; }
    public int HitCount { get; private set; }
    public int SkillLv { get; set; }
    public string Description { get; private set; }
    public float ModifierStart { get; private set; }
    public float Modifier { get { return ModifierStart + (SkillLv - 1) * ModifierIncrease; } }
    public float ModifierNextLevel { get { return ModifierStart + SkillLv * ModifierIncrease; } }
    public int MpCostStart { get; private set; }
    public int MpCost { get { return MpCostStart + (SkillLv - 1) * MpCostIncrease; } }
    public int MpCostNextLevel { get { return MpCostStart + SkillLv * MpCostIncrease; } }
    public int Cooldown { get; private set; }
    public int PriceStart { get; private set; }
    public int Price { get { return PriceStart + (SkillLv - 1) * PriceIncrease; } }
    public int MpCostIncrease { get; private set; }
    public float ModifierIncrease { get; private set; }
    public int MaxSkillLv { get; private set; }
    public int RequireLvIncrease { get; private set; }
    public int PriceIncrease { get; private set; }
    public int RequireLvStart { get; private set; }
    public int RequireLv { get { return RequireLvStart + (SkillLv - 1) * RequireLvIncrease; } }
    public abstract UseOn UseOn { get; }
    public ApplyBuff[] buffs;
    public GameObject Animation { get; private set; }
    public bool IsAOE
    {
        get
        {
            return Type == SkillType.AttackAOE || Type == SkillType.HealAOE || Type == SkillType.BuffAOE || Type == SkillType.DebuffAOE;
        }
    }

    public bool IsUseOnOpponent
    {
        get
        {
            return Type == SkillType.Attack || Type == SkillType.AttackAOE || Type == SkillType.DebuffAOE || Type == SkillType.Debuff;
        }
    }

    public Skill(SkillTemplate template)
    {
        Id = template.id;
        Name = template.skillName;
        Icon = template.icon;
        Type = template.type;
        HitCount = template.hitCount;
        SkillLv = 1;
        Description = template.description;
        ModifierStart = template.modifier;
        MpCostStart = template.mpCost;
        Cooldown = template.cooldown;
        PriceStart = template.price;
        MpCostIncrease = template.mpCostIncrease;
        ModifierIncrease = template.modifierIncrease;
        MaxSkillLv = template.maxSkillLv;
        RequireLvStart = template.requireLv;
        PriceIncrease = template.priceIncrease;
        RequireLvIncrease = template.requireLvIncrease;
        RequireLvStart = template.requireLv;
        Animation = template.animation;
        buffs = new ApplyBuff[template.buffs.Length];
        for (int i = 0; i < template.buffs.Length; i++)
        {
            buffs[i] = new ApplyBuff(template.buffs[i].buffTemplate, template.buffs[i].duration, template.buffs[i].chance);
        }
    }

    public virtual void Use(BattleEntity user, List<BattleEntity> targets)
    {
        user.CurrentMP -= MpCost;
    }

    protected void ApplyBuffs(BattleEntity user, List<BattleEntity> targets)
    {
        if (buffs.Length > 0)
        {
            foreach (ApplyBuff buff in buffs)
            {
                if (UseOn == UseOn.Self)
                {
                    user.AddBuff(buff.buffTemplate, buff.duration);
                }
                else
                {
                    foreach (BattleEntity target in targets)
                    {
                        if (Random.value * 100 < buff.chance)
                        {
                            target.AddBuff(buff.buffTemplate, buff.duration);
                        }
                    }
                }
            }
        }
    }
}
