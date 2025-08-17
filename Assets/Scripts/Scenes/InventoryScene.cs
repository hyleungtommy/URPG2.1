using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InventoryScene : CommonListScene<InventoryListBox>
{
    public ConfirmDisposeDialog confirmDisposeDialog;

    // Start is called before the first frame update
    void Start()
    {
        AddTestItems();
        AddDisplayList(Game.Inventory.StorageSlots.Cast<System.Object>().ToList());
        Render();
        confirmDisposeDialog.gameObject.SetActive(false);
    }

    public override void OnBoxClicked(int slotId){
        StorageSlot slot = displayList[selectedTabId][slotId] as StorageSlot;
        // do nothing if the slot is empty
        if (slot != null && slot.Item != null){
            base.OnBoxClicked(slotId);
        }
    }

    private void AddTestItems()
    {
        // Create test HP Potions
        var hpPotion1 = DBManager.Instance.GetItem(61);
        var hpPotion2 = DBManager.Instance.GetItem(62);
        // Create test MP Potions
        var mpPotion1 = DBManager.Instance.GetItem(6);
        // Add items to inventory
        Game.Inventory.InsertItem(hpPotion1, 99);  // Add 5 Healing Potion I
        Game.Inventory.InsertItem(hpPotion2,99);  // Add 3 Healing Potion II
        Game.Inventory.InsertItem(mpPotion1, 8);  // Add 8 Magic Potion I

    }

}
