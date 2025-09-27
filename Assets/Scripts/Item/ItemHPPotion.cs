using System.Collections.Generic;
using UnityEngine;

public class ItemHPPotion : BattleFunctionalItem
{
    public int HPRestorePercentage;
    public int MinHPRestore;
    public bool AOE;
    public override int MaxStackSize { get { return 10; } }
    public override string ItemType { get { return "HP Potion"; } }
    public override bool IsAOE() { return AOE; }
    public override bool IsUseOnOpponent() { return false; }
    public ItemHPPotion(ItemHPPotionTemplate template) : base(template)
    {
        HPRestorePercentage = template.HPRestorePercentage;
        MinHPRestore = template.MinHPRestore;
        AOE = template.AOE;
    }

    public override void Use(List<BattleEntity> targets)
    {
        // Play heal animation for each target
        foreach (BattleEntity target in targets)
        {
            // Play heal animation at target's position
            if (SkillAnimationManager.Instance != null)
            {
                Vector3 animationPosition = BattleScene.Instance.GetEntityPosition(target);
                SkillAnimationManager.Instance.PlaySkillAnimationByType(SkillType.Heal, animationPosition);
            }
            
            // Apply healing (floating numbers will be shown by BattleEntity.Heal method)
            if (target.Stats.HP <= MinHPRestore)
            {
                target.Heal(MinHPRestore);
            }
            else
            {
                target.Heal((int)(target.Stats.HP * HPRestorePercentage / 100f));
            }
        }
    }
}

