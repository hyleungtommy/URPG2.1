using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BlacksmithShopBox : MonoBehaviour
{
    [SerializeField] BasicItemBox itemBox;
    [SerializeField] Text equipmentName;
    [SerializeField] EquipmentPowerText equipmentPower;
    [SerializeField] Text equipmentPrice;

    public void Render(WeaponTemplate weaponTemplate)
    {
        if(weaponTemplate != null){
            itemBox.Render(weaponTemplate);
            equipmentName.text = weaponTemplate.WeaponName;
            equipmentPower.Render(weaponTemplate);
            equipmentPrice.text = weaponTemplate.Price.ToString();
        }
    }

    public void Render(ArmorTemplate armorTemplate)
    {
        if(armorTemplate != null){
            itemBox.Render(armorTemplate);
            equipmentName.text = armorTemplate.ArmorName;
            equipmentPower.Render(armorTemplate);
            equipmentPrice.text = armorTemplate.Price.ToString();
        }
    }
}
