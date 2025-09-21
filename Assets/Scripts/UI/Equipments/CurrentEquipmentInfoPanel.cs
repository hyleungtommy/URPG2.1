using UnityEngine;
using UnityEngine.UI;

public class CurrentEquipmentInfoPanel : InfoPanel
{
    [SerializeField] Text equipmentNameText;
    [SerializeField] Text equipmentTypeText;
    [SerializeField] Text equipmentExtraEffectText;
    [SerializeField] BasicItemBox equipmentIcon;
    [SerializeField] EquipmentPowerText equipmentPowerText;
    [SerializeField] EquipmentPanel equipmentPanel;

    public override void Render(){
        Equipment equipment = obj as Equipment;
        if(equipment == null) return;
        equipmentNameText.text = equipment.FullName;
        equipmentTypeText.text = FormatEquipmentType(equipment);
        equipmentExtraEffectText.text = "";
        equipmentIcon.Render(equipment);
        equipmentPowerText.Render(equipment);
    }

    public string FormatEquipmentType(Equipment equipment){
        return equipment.ItemType.ToString() + "\n" + (equipment is Armor? (equipment as Armor).ArmorCategory.ToString() : "") + " Armor\nRequire Lv." + equipment.RequireLv;
    }

    public void OnClickUnequip()
    {
        Equipment equipment = obj as Equipment;
        equipmentPanel.character.CharacterClass.EquipmentManager.Unequip(equipment);
        equipmentPanel.Render();
    }
    
    
}