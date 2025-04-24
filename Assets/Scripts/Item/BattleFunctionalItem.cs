using System.Collections.Generic;
public abstract class BattleFunctionalItem : Item
{
    public abstract void Use(List<BattleEntity> targets);
    public abstract bool IsAOE();
    public abstract bool IsUseOnOpponent();

    public BattleFunctionalItem(ItemTemplate template) : base(template)
    {
    }

}

