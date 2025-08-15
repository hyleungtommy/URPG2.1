using UnityEngine;
using UnityEngine.UI;

public class InventoryInfoPanel : InfoPanel
{
    [SerializeField] Text itemNameText;
    [SerializeField] Text itemDescriptionText;
    [SerializeField] Text itemTypeText;
    [SerializeField] Image itemIconImage;

    public override void Render(){
        StorageSlot slot = obj as StorageSlot;
        if (slot.Item != null)
        {
            itemNameText.text = slot.Item.Name;
            itemDescriptionText.text = slot.Item.Description;
            itemTypeText.text = slot.Item.ItemType + "\n" + Constant.itemRarityName[slot.Item.Rarity] + "\nQuantity: " + slot.Quantity;
            itemIconImage.sprite = slot.Item.Icon;
        }
    }
}