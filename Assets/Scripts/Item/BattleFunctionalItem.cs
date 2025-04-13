using System.Collections.Generic;
public abstract class BattleFunctionalItem : Item
{
    public abstract void Use(List<BattleEntity> targets);

    public BattleFunctionalItem(ItemTemplate template) : base(template)
    {
    }

}

