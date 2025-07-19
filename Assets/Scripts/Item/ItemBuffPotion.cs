using System.Collections.Generic;
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
        foreach (BattleEntity target in targets){
            target.AddBuff(buffTemplate, duration);
        }
    }
}