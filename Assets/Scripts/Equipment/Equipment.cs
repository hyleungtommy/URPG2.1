using UnityEngine;

public abstract class Equipment:Item{
    public int RequireLv {get; private set;}

    public override int MaxStackSize {get{return 1;}}

    public Equipment(string equipmentName, string description, Sprite icon, int price, int requireLv)
    :base(0, equipmentName, description, price, 0, 0, icon){
        this.RequireLv = requireLv;
    }

    public abstract BaseStat GetStat();
}

public class Weapon: Equipment{
    public WeaponType WeaponType;
    public int Damage {get; private set;}
    public int MagicDamage {get; private set;}
    public override string ItemType {get{return WeaponType.ToString();}}

    public Weapon(ShopWeapon shopWeapon): base(shopWeapon.EquipmentName, shopWeapon.Description, shopWeapon.Icon, shopWeapon.Price, shopWeapon.RequireLv){
        this.WeaponType = shopWeapon.WeaponType;
        this.Damage = shopWeapon.Damage;
        this.MagicDamage = shopWeapon.MagicDamage;
    }

    public override BaseStat GetStat(){
        return new BaseStat(0, 0, Damage, MagicDamage, 0, 0, 0, 0);
    }
}

public class Armor: Equipment{
    public ArmorType ArmorType;
    public ArmorCategory ArmorCategory;
    public int Defense {get; private set;}
    public int MagicDefense {get; private set;}
    public override string ItemType {get{return ArmorType.ToString();}}

    public Armor(ShopArmor shopArmor): base(shopArmor.EquipmentName, shopArmor.Description, shopArmor.Icon, shopArmor.Price, shopArmor.RequireLv){
        this.ArmorType = shopArmor.ArmorType;
        this.ArmorCategory = shopArmor.ArmorCategory;
        this.Defense = shopArmor.Defense;
        this.MagicDefense = shopArmor.MagicDefense;
    }

    public override BaseStat GetStat(){
        return new BaseStat(0, 0, 0, 0, Defense, MagicDefense, 0, 0);
    }
}


