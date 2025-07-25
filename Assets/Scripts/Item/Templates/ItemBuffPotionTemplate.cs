using UnityEngine;
[CreateAssetMenu(fileName = "ItemBuffPotionTemplate", menuName = "Item/Buff Potion")]
public class ItemBuffPotionTemplate : ItemTemplate
{
    public BuffTemplate buffTemplate;
    public int duration;

    public override Item GetItem(){
        return new ItemBuffPotion(this);
    }

    public override string GetItemType()
    {
        return "Buff Potion";
    }
}