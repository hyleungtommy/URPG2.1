using UnityEngine;

public abstract class Equipment:Item{
    public int RequireLv {get; private set;}
    public int ReinforceLv {get; private set;}
    //Full name: Rarity + Name + enchantment + ReinforceLv (e.g. Epic Iron Sword of Power +1)
    public string FullName {get{return Constant.equipmentRarityName[Rarity] + " " + Name + "+" + ReinforceLv;}}

    public override int MaxStackSize {get{return 1;}}

    public Equipment(string equipmentName, string description, Sprite icon, int price, int requireLv)
    :base(0, equipmentName, description, price, 0, 0, icon){
        this.RequireLv = requireLv;
        this.ReinforceLv = 0;
    }

    public abstract BaseStat GetStat();
}

public class Weapon: Equipment{
    public WeaponType WeaponType;
    public int Damage {get; private set;}
    public int MagicDamage {get; private set;}
    public override string ItemType {get{return WeaponType.ToString();}}

    public Weapon(WeaponTemplate weaponTemplate): base(weaponTemplate.Name, weaponTemplate.Description, weaponTemplate.Icon, weaponTemplate.Price, weaponTemplate.requireLv){
        this.WeaponType = weaponTemplate.WeaponType;
        this.Damage = weaponTemplate.Damage;
        this.MagicDamage = weaponTemplate.MagicDamage;
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

    public Armor(ArmorTemplate armorTemplate): base(armorTemplate.Name, armorTemplate.Description, armorTemplate.Icon, armorTemplate.Price, armorTemplate.requireLv){
        this.ArmorType = armorTemplate.ArmorType;
        this.ArmorCategory = armorTemplate.ArmorCategory;
        this.Defense = armorTemplate.Defense;
        this.MagicDefense = armorTemplate.MagicDefense;
    }

    public override BaseStat GetStat(){
        return new BaseStat(0, 0, 0, 0, Defense, MagicDefense, 0, 0);
    }
}


