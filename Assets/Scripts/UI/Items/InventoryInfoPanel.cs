using UnityEngine;
using UnityEngine.UI;

public class InventoryInfoPanel : InfoPanel
{
    [SerializeField] Text itemNameText;
    [SerializeField] Text itemDescriptionText;
    [SerializeField] Text itemTypeText;
    [SerializeField] Image itemIconImage;
    [SerializeField] InventoryScene inventoryScene;
    [SerializeField] Text equipmentEnchantmentText;
    [SerializeField] EquipmentPowerText equipmentPowerText;

    public override void Render(){
        StorageSlot slot = obj as StorageSlot;
        if (slot.Item != null)
        {
            if(slot.Item is Equipment){
                itemNameText.text = (slot.Item as Equipment).FullName;
                FormatEnchantment(slot.Item as Equipment);
                equipmentPowerText.gameObject.SetActive(true);
                equipmentPowerText.Render(slot.Item as Equipment);
            }else{
                itemNameText.text = slot.Item.Name;
                equipmentEnchantmentText.text = "";
                equipmentPowerText.gameObject.SetActive(false);
            }
            itemDescriptionText.text = slot.Item.Description;
            itemTypeText.text = slot.Item.ItemType + "\n" + Constant.itemRarityName[slot.Item.Rarity] + "\nQuantity: " + slot.Quantity;
            itemIconImage.sprite = slot.Item.Icon;
        }
    }

    public void FormatEnchantment(Equipment equipment){
        if(equipment.Enchantments.Count > 0){
            equipmentEnchantmentText.text = "Enchantment: " + equipment.Enchantments[0].Name + " " + Util.NumberToRoman(equipment.Enchantments[0].Level) + "\n" + equipment.Enchantments[0].Description;
        }else{
            equipmentEnchantmentText.text = "";
        }
    }

    public void OnClickDispose(){
        StorageSlot slot = obj as StorageSlot;
        inventoryScene.confirmDisposeDialog.Setup(slot);
        inventoryScene.confirmDisposeDialog.gameObject.SetActive(true);
    }
}