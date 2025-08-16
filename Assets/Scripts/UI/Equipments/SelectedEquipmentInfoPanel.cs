using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedEquipmentInfoPanel : InfoPanel
{
    [SerializeField] Text equipmentNameText;
    [SerializeField] Text equipmentTypeText;
    [SerializeField] Text equipmentExtraEffectText;
    [SerializeField] BasicItemBox equipmentIcon;
    [SerializeField] EquipmentPowerText equipmentPowerText;
    [SerializeField] Text errorText;
    [SerializeField] Button equipButton;
    [SerializeField] EquipmentPanel equipmentPanel;
    public override void Render(){
        StorageSlot slot = obj as StorageSlot;
        Equipment equipment = slot.Item as Equipment;
        if(equipment == null) return;
        equipmentNameText.text = equipment.Name;
        equipmentTypeText.text = FormatEquipmentType(equipment);
        equipmentExtraEffectText.text = "";
        equipmentIcon.Render(equipment);
        equipmentPowerText.Render(equipment);
        equipButton.gameObject.SetActive(CanEquip());
    }

    public string FormatEquipmentType(Equipment equipment){
        return equipment.ItemType.ToString() + "\n" + (equipment is Armor? (equipment as Armor).ArmorCategory.ToString() : "") + " Armor\nRequire Lv." + equipment.RequireLv;
    }

    private bool CanEquip()
    {
        StorageSlot slot = obj as StorageSlot;
        Equipment equipment = slot.Item as Equipment;
        if (equipment == null)
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "Please select an equipment";
            return false;
        }
        if (equipment.RequireLv > equipmentPanel.character.Lv)
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "Require Lv." + equipment.RequireLv;
            return false;
        }
        errorText.gameObject.SetActive(false);
        return true;
    }

    public void OnClickEquip()
    {
        StorageSlot slot = obj as StorageSlot;
        Equipment equipment = slot.Item as Equipment;
        equipmentPanel.character.CharacterClass.EquipmentManager.Equip(equipment);
        slot.Clear();
        equipmentPanel.Render();
    }
}
