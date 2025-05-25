using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentPowerText : MonoBehaviour
{
    [SerializeField] GameObject weaponDamage;
    [SerializeField] GameObject weaponMagicDamage;
    [SerializeField] GameObject armorDefense;
    [SerializeField] GameObject armorMagicDefense;
    [SerializeField] Text weaponDamageText;
    [SerializeField] Text weaponMagicDamageText;
    [SerializeField] Text armorDefenseText;
    [SerializeField] Text armorMagicDefenseText;
    public void Render(Equipment equipment){
        if(equipment is Weapon){
            Weapon weapon = equipment as Weapon;
            weaponDamage.SetActive(true);
            weaponMagicDamage.SetActive(true);
            armorDefense.SetActive(false);
            armorMagicDefense.SetActive(false);
            weaponDamageText.text = weapon.Damage.ToString();
            weaponMagicDamageText.text = weapon.MagicDamage.ToString();
        }
        else if(equipment is Armor){
            Armor armor = equipment as Armor;
            weaponDamage.SetActive(false);
            weaponMagicDamage.SetActive(false);
            armorDefense.SetActive(true);
            armorMagicDefense.SetActive(true);
            armorDefenseText.text = armor.Defense.ToString();
            armorMagicDefenseText.text = armor.MagicDefense.ToString();
        }
    }

    public void Render(WeaponTemplate weaponTemplate){
        weaponDamage.SetActive(true);
        weaponMagicDamage.SetActive(true);
        armorDefense.SetActive(false);
        armorMagicDefense.SetActive(false);
        weaponDamageText.text = weaponTemplate.Damage.ToString();
    }

    public void Render(ArmorTemplate armorTemplate){
        weaponDamage.SetActive(false);
        weaponMagicDamage.SetActive(false);
        armorDefense.SetActive(true);
        armorMagicDefense.SetActive(true);
        armorDefenseText.text = armorTemplate.Defense.ToString();
    }

    public void RenderNull(){
        weaponDamage.SetActive(false);
        weaponMagicDamage.SetActive(false);
        armorDefense.SetActive(false);
        armorMagicDefense.SetActive(false);
    }
}
