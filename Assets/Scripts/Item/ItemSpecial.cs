using System.Collections.Generic;
public class ItemSpecial : BattleFunctionalItem{
    public override int MaxStackSize { get { return 10; } }
    public override string ItemType { get { return "Special"; } }
    public override bool IsAOE() { return false; }
    public override bool IsUseOnOpponent() { return false; }
    public ItemSpecial(ItemSpecialTemplate template) : base(template)
    {

    }

    public override void Use(List<BattleEntity> targets){

    }
}