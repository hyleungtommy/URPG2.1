using System.Collections.Generic;
using UnityEngine;

public class ItemBuffPotion : BattleFunctionalItem{
    public BuffTemplate buffTemplate;
    public int duration;
    public override int MaxStackSize { get { return 10; } }
    public override string ItemType { get { return "Buff Potion"; } }
    public override bool IsAOE() { return false; }
    public override bool IsUseOnOpponent() { return false; }
    public ItemBuffPotion(ItemBuffPotionTemplate template) : base(template)
    {
        buffTemplate = template.buffTemplate;
        duration = template.duration;
    }

    public override void Use(List<BattleEntity> targets){
        // Play buff animation for each target
        foreach (BattleEntity target in targets){
            // Play buff animation at target's position
            if (SkillAnimationManager.Instance != null)
            {
                Vector3 animationPosition = BattleScene.Instance.GetEntityPosition(target);
                SkillAnimationManager.Instance.PlaySkillAnimationByType(SkillType.Buff, animationPosition);
            }
            
            // Apply buff (no floating numbers for buffs as requested)
            target.AddBuff(buffTemplate, duration);
        }
    }
}