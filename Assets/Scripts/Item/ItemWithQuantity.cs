using UnityEngine;

public class ItemWithQuantity
{
    public Item Item { get; private set; }
    public int Quantity { get; private set; }

    public ItemWithQuantity(Item item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }
}



