using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BlacksmithShopBox : ListBox
{
    [SerializeField] BasicItemBox itemBox;
    [SerializeField] Text equipmentName;
    [SerializeField] EquipmentPowerText equipmentPower;
    [SerializeField] Text equipmentPrice;

    public override void Render(){
        if(obj is WeaponTemplate){
            Render(obj as WeaponTemplate);
        }
        else if(obj is ArmorTemplate){
            Render(obj as ArmorTemplate);
        }
    }

    private void Render(WeaponTemplate weaponTemplate)
    {
        if(weaponTemplate != null){
            itemBox.Render(weaponTemplate);
            equipmentName.text = weaponTemplate.WeaponName;
            equipmentPower.Render(weaponTemplate);
            equipmentPrice.text = weaponTemplate.Price.ToString();
        }
    }

    private void Render(ArmorTemplate armorTemplate)
    {
        if(armorTemplate != null){
            itemBox.Render(armorTemplate);
            equipmentName.text = armorTemplate.ArmorName;
            equipmentPower.Render(armorTemplate);
            equipmentPrice.text = armorTemplate.Price.ToString();
        }
    }
}
