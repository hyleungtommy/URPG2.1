using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Equipment/Armor")]
public class ArmorTemplate: ItemTemplate
{
    public ArmorType ArmorType;
    public ArmorCategory ArmorCategory;
    public int Defense;
    public int MagicDefense;
    public int requireLv;

    public override Item GetItem()
    {
        return new Armor(this);
    }

    public override string GetItemType()
    {
        return "Armor";
    }
}