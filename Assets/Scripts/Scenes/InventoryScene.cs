using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScene : MonoBehaviour
{
    [SerializeField] GameObject InventoryBoxPrefab;
    [SerializeField] Transform InventoryBoxContainer;
    [SerializeField] Text ItemName;
    [SerializeField] Text ItemDescription;
    [SerializeField] Text ItemType;
    [SerializeField] Image ItemIcon;
    [SerializeField] Image ItemIconFrame;
    private StorageSystem inventory;
    private List<InventoryBox> inventoryBoxes = new List<InventoryBox>();

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameController.Instance.Inventory;
        
        // Test code to populate inventory
        //AddTestItems();
        
        Render();
    }

    private void AddTestItems()
    {
        // Create test HP Potions
        var hpPotion1 = DBManager.Instance.GetItem(1);
        var hpPotion2 = DBManager.Instance.GetItem(2);
        // Create test MP Potions
        var mpPotion1 = DBManager.Instance.GetItem(6);
        // Add items to inventory
        inventory.InsertItem(hpPotion1, 5);  // Add 5 Healing Potion I
        inventory.InsertItem(hpPotion2, 3);  // Add 3 Healing Potion II
        inventory.InsertItem(mpPotion1, 8);  // Add 8 Magic Potion I
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Render()
    {
        // Clear existing boxes
        foreach (Transform child in InventoryBoxContainer)
        {
            Destroy(child.gameObject);
        }
        inventoryBoxes.Clear();

        // Create new boxes for each slot
        for (int i = 0; i < inventory.Size; i++)
        {
            int j = i;
            GameObject boxObj = Instantiate(InventoryBoxPrefab, InventoryBoxContainer);
            InventoryBox box = boxObj.GetComponent<InventoryBox>();
            box.Setup(inventory.StorageSlots[i]);
            box.GetComponent<Button>().onClick.AddListener(() => this.OnInventoryBoxClicked(j));
            box.Render();
            inventoryBoxes.Add(box);
        }
    }

    private void OnInventoryBoxClicked(int slotId)
    {
        if (inventory.StorageSlots[slotId].Item != null)
        {
            ItemName.text = inventory.StorageSlots[slotId].Item.Name;
            ItemDescription.text = inventory.StorageSlots[slotId].Item.Description;
            FormatItemDetails(slotId);
            ItemIcon.sprite = inventory.StorageSlots[slotId].Item.Icon;
            ItemIcon.gameObject.SetActive(true);
            ItemIconFrame.gameObject.SetActive(true);
        }
        else
        {
            ClearItemDetails();
        }
    }

    private void FormatItemDetails(int slotId)
    {
        ItemType.text = inventory.StorageSlots[slotId].Item.ItemType + "\n" + Constant.itemRarityName[inventory.StorageSlots[slotId].Item.Rarity] + "\nQuantity: " + inventory.StorageSlots[slotId].Quantity;
    }

    private void ClearItemDetails()
    {
        ItemName.text = "";
        ItemDescription.text = "";
        ItemType.text = "";
        ItemIcon.gameObject.SetActive(false);
        ItemIconFrame.gameObject.SetActive(false);
    }
}
