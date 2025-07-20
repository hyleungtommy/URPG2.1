using UnityEngine;
[CreateAssetMenu(fileName = "ItemHPPotionTemplate", menuName = "Item/HP Potion")]
public class ItemHPPotionTemplate : ItemTemplate
{
    public int HPRestorePercentage;
    public int MinHPRestore;
    public bool AOE;

    public override Item GetItem()
    {
        return new ItemHPPotion(this);
    }

    public override string GetItemType()
    {
        return "HP Potion";
    }
}
