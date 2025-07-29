using UnityEngine;
[CreateAssetMenu(fileName = "ItemSpecialTemplate", menuName = "Item/Special")]
public class ItemSpecialTemplate : ItemTemplate{
    public override Item GetItem()
    {
        return new ItemSpecial(this);
    }

    public override string GetItemType()
    {
        return "Special";
    }
}