using System.Collections.Generic;
using UnityEngine;

public abstract class ShopEquipment{
    public string EquipmentName{get; private set;}
    public string Description {get; private set;}
    public Sprite Icon {get; private set;}
    public int Price {get; private set;}
    public int RequireLv {get; private set;}

    public ShopEquipment(string equipmentName, string description, Sprite icon, int price, int requireLv){
        this.EquipmentName = equipmentName;
        this.Description = description;
        this.Icon = icon;
        this.Price = price;
        this.RequireLv = requireLv;
    }
}

public class ShopWeapon: ShopEquipment{
    public WeaponType WeaponType;
    public int Damage;
    public int MagicDamage;

    public ShopWeapon(WeaponTemplate weaponTemplate): base(weaponTemplate.WeaponName, weaponTemplate.Description, weaponTemplate.Icon, weaponTemplate.Price, weaponTemplate.requireLv){
        this.WeaponType = weaponTemplate.WeaponType;
        this.Damage = weaponTemplate.Damage;
        this.MagicDamage = weaponTemplate.MagicDamage;
    }
}

public class ShopArmor: ShopEquipment{
    public ArmorType ArmorType;
    public ArmorCategory ArmorCategory;
    public int Defense;
    public int MagicDefense;

    public ShopArmor(ArmorTemplate armorTemplate): base(armorTemplate.ArmorName, armorTemplate.Description, armorTemplate.Icon, armorTemplate.Price, armorTemplate.requireLv){
        this.ArmorType = armorTemplate.ArmorType;
        this.ArmorCategory = armorTemplate.ArmorCategory;
        this.Defense = armorTemplate.Defense;
        this.MagicDefense = armorTemplate.MagicDefense;
    }
}