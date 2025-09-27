using System.Collections.Generic;
using UnityEngine;

public class ItemMPPotion : BattleFunctionalItem
{
    public int MPRestorePercentage;
    public int MinMPRestore;
    public bool AOE;
    public override int MaxStackSize { get { return 10; } }
    public override string ItemType { get { return "MP Potion"; } }
    public override bool IsAOE() { return AOE; }
    public override bool IsUseOnOpponent() { return false; }
    public ItemMPPotion(ItemMPPotionTemplate template) : base(template)
    {
        MPRestorePercentage = template.MPRestorePercentage;
        MinMPRestore = template.MinMPRestore;
        AOE = template.AOE;
    }

    public override void Use(List<BattleEntity> targets)
    {
        // Play heal animation for each target (using heal animation for MP potions as requested)
        foreach (BattleEntity target in targets)
        {
            // Play heal animation at target's position
            if (SkillAnimationManager.Instance != null)
            {
                Vector3 animationPosition = BattleScene.Instance.GetEntityPosition(target);
                SkillAnimationManager.Instance.PlaySkillAnimationByType(SkillType.Heal, animationPosition);
            }
            
            // Apply MP restoration (floating numbers will be shown by BattleEntity.RestoreMP method)
            if (target.Stats.MP <= MinMPRestore)
            {
                target.RestoreMP(MinMPRestore);
            }
            else
            {
                target.RestoreMP((int)(target.Stats.MP * MPRestorePercentage / 100f));
            }
        }
    }

}

