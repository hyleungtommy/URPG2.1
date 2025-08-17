using System.Collections.Generic;
using System.Linq;

public class StorageSystem
{
    public List<StorageSlot> StorageSlots;
    public int Size { get { return StorageSlots.Count; } }

    public StorageSystem(int size)
    {
        StorageSlots = new List<StorageSlot>(size);
        for (int i = 0; i < size; i++)
        {
            StorageSlots.Add(new StorageSlot());
        }
    }

    public int InsertItem(Item item, int quantity)
    {
        // First try to add to existing slots with the same item id
        foreach (var slot in StorageSlots)
        {
            if (slot.Item != null && slot.Item.id == item.id && slot.Quantity < slot.Item.MaxStackSize)
            {
                int spaceInSlot = slot.Item.MaxStackSize - slot.Quantity;
                int amountToAdd = System.Math.Min(spaceInSlot, quantity);

                slot.Quantity += amountToAdd;
                quantity -= amountToAdd;

                if (quantity == 0)
                    return 0;
            }
        }

        // Then try to add to empty slots
        foreach (var slot in StorageSlots)
        {
            if (slot.Item == null)
            {
                slot.Item = item;
                int amountToAdd = System.Math.Min(item.MaxStackSize, quantity);
                slot.Quantity = amountToAdd;
                quantity -= amountToAdd;

                if (quantity == 0)
                    return 0;
            }
        }

        // Return remaining quantity that couldn't be added
        return quantity;
    }

    public int RemoveItem(Item item, int quantity)
    {
        // First check if we have enough items
        int totalQuantity = GetTotalItemQuantity(item);

        if (totalQuantity < quantity)
        {
            return 0; // Not enough items, return 0 to indicate no items were removed
        }

        int remainingToRemove = quantity;

        // Get all slots containing the item in their natural order
        var relevantSlots = new List<StorageSlot>();
        
        if(item is Weapon){
            relevantSlots = StorageSlots
                .Where(slot => slot.Item != null && slot.Item is Weapon && slot.Item.id == item.id)
                .ToList();
        }else if(item is Armor){
            relevantSlots = StorageSlots
                .Where(slot => slot.Item != null && slot.Item is Armor && slot.Item.id == item.id)
                .ToList();
        }
        else{
            relevantSlots = StorageSlots
                .Where(slot => slot.Item != null && slot.Item.id == item.id)
                .ToList();
        }

        foreach (var slot in relevantSlots)
        {
            if (remainingToRemove <= 0)
                break;

            int amountToRemove = System.Math.Min(slot.Quantity, remainingToRemove);
            slot.Quantity -= amountToRemove;
            remainingToRemove -= amountToRemove;

            // Clear the slot if it's empty
            if (slot.Quantity == 0)
            {
                slot.Item = null;
            }
        }

        // Return the quantity that was removed (should be equal to the requested quantity)
        return quantity;
    }

    public int GetTotalItemQuantity(Item item)
    {
        if (item is Weapon)
        {
            StorageSlots
           .Where(slot => slot.Item != null && slot.Item is Weapon && slot.Item.id == item.id)
           .Sum(slot => slot.Quantity);
        }
        else if (item is Armor)
        {
            return StorageSlots
            .Where(slot => slot.Item != null && slot.Item is Armor && slot.Item.id == item.id)
            .Sum(slot => slot.Quantity);
        }
        return StorageSlots
            .Where(slot => slot.Item != null && slot.Item.id == item.id)
            .Sum(slot => slot.Quantity);
    }

    public List<ItemWithQuantity> GetBattleFunctionalItemList()
    {
        List<ItemWithQuantity> battleFunctionalItems = new List<ItemWithQuantity>();
        foreach (var slot in StorageSlots)
        {
            if (slot.Item != null && slot.Item is BattleFunctionalItem)
            {
                battleFunctionalItems.Add(new ItemWithQuantity(slot.Item, slot.Quantity));
            }
        }
        return battleFunctionalItems;
    }

    public List<StorageSlot> GetEquipmentList()
    {
        List<StorageSlot> equipmentList = new List<StorageSlot>();
        foreach (var slot in StorageSlots)
        {
            if (slot.Item != null && slot.Item is Equipment)
            {
                equipmentList.Add(slot);
            }
        }
        return equipmentList;
    }

}





