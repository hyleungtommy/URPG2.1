using UnityEngine;
using UnityEngine.UI;


public class SmithingListBox : ListBox{
    [SerializeField] Text itemNameText;
    [SerializeField] BasicItemBox itemBox;
    [SerializeField] EquipmentPowerText equipmentPowerText;
    public override void Render(){
        var item = obj as CraftRecipe;
        Equipment equipment = item.resultItem as Equipment;
        itemNameText.text = equipment.Name;
        itemBox.Render(equipment);
        equipmentPowerText.Render(equipment);
    }
}