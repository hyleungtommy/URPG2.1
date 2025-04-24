using UnityEngine;

public abstract class Item
{
    public int id;
    public string Name;
    public string Description;
    public int Price;
    public int SellPrice;
    public int Rarity;
    public Sprite Icon;
    public abstract int MaxStackSize { get; }
    public abstract string ItemType { get; }

    public Item(ItemTemplate template)
    {
        id = template.id;
        Name = template.Name;
        Description = template.Description;
        Price = template.Price;
        SellPrice = template.SellPrice;
        Rarity = template.Rarity;
        Icon = template.Icon;
    }
}


