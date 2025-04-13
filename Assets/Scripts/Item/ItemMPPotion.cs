using System.Collections.Generic;
public class ItemMPPotion : BattleFunctionalItem
{
    public int MPRestorePercentage;
    public int MinMPRestore;
    public bool AOE;
    public override int MaxStackSize { get { return 10; } }
    public ItemMPPotion(ItemMPPotionTemplate template) : base(template)
    {
        MPRestorePercentage = template.MPRestorePercentage;
        MinMPRestore = template.MinMPRestore;
        AOE = template.AOE;
    }

    public override void Use(List<BattleEntity> targets)
    {
        foreach (BattleEntity target in targets)
        {
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

