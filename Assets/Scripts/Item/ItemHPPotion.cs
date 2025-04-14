using System.Collections.Generic;
public class ItemHPPotion : BattleFunctionalItem
{
    public int HPRestorePercentage;
    public int MinHPRestore;
    public bool AOE;
    public override int MaxStackSize { get { return 10; } }
    public override string ItemType { get { return "HP Potion"; } }
    public ItemHPPotion(ItemHPPotionTemplate template) : base(template)
    {
        HPRestorePercentage = template.HPRestorePercentage;
        MinHPRestore = template.MinHPRestore;
        AOE = template.AOE;
    }

    public override void Use(List<BattleEntity> targets)
    {
        foreach (BattleEntity target in targets)
        {
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

