using UnityEngine;
using System.Collections.Generic;
public abstract class Equipment:Item{
    public int RequireLv {get; private set;}
    public int ReinforceLv {get; private set;}
    public List<Enchantment> Enchantments {get; private set;}
    //Full name: Rarity + Name + enchantment + ReinforceLv (e.g. Epic Iron Sword of Power +1)
    public string FullName {get{return Constant.equipmentRarityName[Rarity] + " " + Name + (Enchantments.Count > 0 ? " of " + Enchantments[0].Name : "") + (ReinforceLv > 0 ? "+" + ReinforceLv : "");}}

    public override int MaxStackSize {get{return 1;}}

    public Equipment(string equipmentName, string description, Sprite icon, int price, int requireLv)
    :base(0, equipmentName, description, price, 0, 0, icon){
        this.RequireLv = requireLv;
        this.ReinforceLv = 0;
        this.Enchantments = new List<Enchantment>();
    }

    public abstract Equipment CreateCopy();

    public void Reinforce(){
        ReinforceLv++;
    }

    public void Enchant(){
        List<Enchantment> enchantments = ReinforceManager.GetRandomEnchantment(this);
        foreach(Enchantment enchantment in enchantments){
            this.Enchantments.Add(enchantment);
        }
    }

    public EnchantmentStat GetEnchantmentStat(){
        EnchantmentStat stat = new EnchantmentStat();
        foreach(Enchantment enchantment in Enchantments){
            stat = stat.Add(enchantment.GetStat());
        }
        return stat;
    }

    public abstract BaseStat GetStat();
}

public class Weapon: Equipment{
    public WeaponType WeaponType;
    public int RawDamage {get; private set;}
    public int RawMagicDamage {get; private set;}
    public int Damage {get {return RawDamage + ReinforceManager.GetReinforcePowerIncrease(this) * ReinforceLv;}}
    public int MagicDamage {get {return RawMagicDamage + ReinforceManager.GetReinforceMagicPowerIncrease(this) * ReinforceLv;}}
    public override string ItemType {get{return WeaponType.ToString();}}

    public Weapon(WeaponTemplate weaponTemplate): base(weaponTemplate.Name, weaponTemplate.Description, weaponTemplate.Icon, weaponTemplate.Price, weaponTemplate.requireLv){
        this.WeaponType = weaponTemplate.WeaponType;
        this.RawDamage = weaponTemplate.Damage;
        this.RawMagicDamage = weaponTemplate.MagicDamage;
    }

    public Weapon(Weapon weapon): base(weapon.Name, weapon.Description, weapon.Icon, weapon.Price, weapon.RequireLv){
        this.WeaponType = weapon.WeaponType;
        this.RawDamage = weapon.RawDamage;
        this.RawMagicDamage = weapon.RawMagicDamage;
    }

    public override BaseStat GetStat(){
        return new BaseStat(0, 0, (int)(Damage * Constant.equipmentRarityMultiplier[Rarity]), (int)(MagicDamage * Constant.equipmentRarityMultiplier[Rarity]), 0, 0, 0, 0);
    }

    public override Equipment CreateCopy(){
        return new Weapon(this);
    }
}

public class Armor: Equipment{
    public ArmorType ArmorType;
    public ArmorCategory ArmorCategory;
    public int RawDefense {get; private set;}
    public int RawMagicDefense {get; private set;}
    public int Defense {get {return RawDefense + ReinforceManager.GetReinforcePowerIncrease(this) * ReinforceLv;}}
    public int MagicDefense {get {return RawMagicDefense + ReinforceManager.GetReinforceMagicPowerIncrease(this) * ReinforceLv;}}
    public override string ItemType {get{return ArmorType.ToString();}}

    public Armor(ArmorTemplate armorTemplate): base(armorTemplate.Name, armorTemplate.Description, armorTemplate.Icon, armorTemplate.Price, armorTemplate.requireLv){
        this.ArmorType = armorTemplate.ArmorType;
        this.ArmorCategory = armorTemplate.ArmorCategory;
        this.RawDefense = armorTemplate.Defense;
        this.RawMagicDefense = armorTemplate.MagicDefense;
    }

    public Armor(Armor armor): base(armor.Name, armor.Description, armor.Icon, armor.Price, armor.RequireLv){
        this.ArmorType = armor.ArmorType;
        this.ArmorCategory = armor.ArmorCategory;
        this.RawDefense = armor.RawDefense;
        this.RawMagicDefense = armor.RawMagicDefense;
    }

    public override BaseStat GetStat(){
        return new BaseStat(0, 0, 0, 0, (int)(Defense * Constant.equipmentRarityMultiplier[Rarity]), (int)(MagicDefense * Constant.equipmentRarityMultiplier[Rarity]), 0, 0);
    }

    public override Equipment CreateCopy(){
        return new Armor(this);
    }
}


