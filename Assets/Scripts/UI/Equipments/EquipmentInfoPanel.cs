using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentInfoPanel : MonoBehaviour
{
    [SerializeField] Text equipmentNameText;
    [SerializeField] Text equipmentTypeText;
    [SerializeField] Text equipmentExtraEffectText;
    [SerializeField] BasicItemBox equipmentIcon;
    [SerializeField] EquipmentPowerText equipmentPowerText;
    public void Render(Equipment equipment){
        if(equipment == null) return;
        equipmentNameText.text = equipment.Name;
        equipmentTypeText.text = FormatEquipmentType(equipment);
        equipmentExtraEffectText.text = "";
        equipmentIcon.Render(equipment);
        equipmentPowerText.Render(equipment);
    }

    public void Clear(){
        equipmentNameText.text = "";
        equipmentTypeText.text = "";
        equipmentExtraEffectText.text = "";
        equipmentIcon.RenderNull();
        equipmentPowerText.RenderNull();
    }

    public string FormatEquipmentType(Equipment equipment){
        return equipment.ItemType.ToString() + "\n" + (equipment is Armor? (equipment as Armor).ArmorCategory.ToString() : "") + " Armor\nRequire Lv." + equipment.RequireLv;
    }

    public void Hide(){
        gameObject.SetActive(false);
    }

    public void Show(){
        gameObject.SetActive(true);
    }
}
