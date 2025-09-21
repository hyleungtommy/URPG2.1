using System.Linq;
using System.Collections.Generic;
using UnityEngine;
public static class ReinforceManager
{
    public static bool CanReinforce(Equipment equipment)
    {
        // an equipment can be reinforced if there is a matching criteria in ReinforceDataTemplate, and is lower than the max reinforce level
        ReinforceData reinforceData = DBManager.Instance.GetReinforceData(GetReinforceEquipmentType(equipment), equipment.RequireLv);
        if (reinforceData == null)
        {
            return false;
        }
        else
        {
            return equipment.ReinforceLv < reinforceData.maxReinforceLevel;
        }
    }

    public static int GetReinforcePowerIncrease(Equipment equipment)
    {
        if(equipment is Weapon){
            Weapon weapon = equipment as Weapon;
            int increase = (int)(weapon.RawDamage * Constant.ReinforcePowerIncreasePercent / 100);
            return (increase == 0 ? 1 : increase);
        }
        else if(equipment is Armor){
            Armor armor = equipment as Armor;
            int increase = (int)(armor.RawDefense * Constant.ReinforcePowerIncreasePercent / 100);
            return (increase == 0 ? 1 : increase);
        }
        return 0;
    }

    public static int GetReinforceMagicPowerIncrease(Equipment equipment)
    {
        if(equipment is Weapon){
            Weapon weapon = equipment as Weapon;
            int increase = (int)(weapon.RawMagicDamage * Constant.ReinforcePowerIncreasePercent / 100);
            return (increase == 0 ? 1 : increase);
        }
        else if(equipment is Armor){
            Armor armor = equipment as Armor;
            int increase = (int)(armor.RawMagicDefense * Constant.ReinforcePowerIncreasePercent / 100);
            return (increase == 0 ? 1 : increase);
        }
        return 0;
    }

    public static ReinforceEquipmentType GetReinforceEquipmentType(Equipment equipment)
    {
        if (equipment is Weapon)
        {
            WeaponType weaponType = (equipment as Weapon).WeaponType;
            switch (weaponType)
            {
                case WeaponType.Sword:
                    return ReinforceEquipmentType.WeaponMelee;
                case WeaponType.Shield:
                    return ReinforceEquipmentType.WeaponMelee;
                case WeaponType.Axe:
                    return ReinforceEquipmentType.WeaponMelee;
                case WeaponType.Bow:
                    return ReinforceEquipmentType.WeaponRanged;
                case WeaponType.Dagger:
                    return ReinforceEquipmentType.WeaponRanged;
                case WeaponType.Wand:
                    return ReinforceEquipmentType.WeaponMagic;
                case WeaponType.Staff:
                    return ReinforceEquipmentType.WeaponMagic;
                case WeaponType.Artifact:
                    return ReinforceEquipmentType.WeaponMagic;
                default:
                    return ReinforceEquipmentType.WeaponMelee;

            }
        }
        else if (equipment is Armor)
        {
            ArmorCategory armorCategory = (equipment as Armor).ArmorCategory;
            switch (armorCategory)
            {
                case ArmorCategory.Heavy:
                    return ReinforceEquipmentType.HeavyArmor;
                case ArmorCategory.Medium:
                    return ReinforceEquipmentType.MediumArmor;
                case ArmorCategory.Light:
                    return ReinforceEquipmentType.LightArmor;
                default:
                    return ReinforceEquipmentType.LightArmor;
            }
        }
        return ReinforceEquipmentType.WeaponMelee;
    }

    public static bool CanEnchant(Equipment equipment)
    {
        EnchantmentData enchantmentData = DBManager.Instance.GetEnchantmentData(equipment.RequireLv);
        if (enchantmentData == null)
        {
            return false;
        }
        else
        {
            return equipment.Enchantments.Count  == 0;
        }
    }

    public static List<Enchantment> GetRandomEnchantment(Equipment equipment)
    {
        List<Enchantment> enchantments = new List<Enchantment>();
        EnchantmentData enchantmentData = DBManager.Instance.GetEnchantmentData(equipment.RequireLv);
        if (enchantmentData == null)
        {
            Debug.LogError("No enchantment data found for equipment level: " + equipment.RequireLv);
            return enchantments;
        }
        else
        {
            List<EnchantmentTemplate> possibleEnchantments = DBManager.Instance.GetAllEnchantmentTemplates().Where(e => e.applyType == EnchantmentApplyType.All || e.applyType == GetEnchantmentApplyType(equipment)).ToList();
            
            // Check if there are any possible enchantments before selecting one
            if (possibleEnchantments.Count == 0)
            {
                Debug.LogError("No possible enchantments found for equipment level: " + equipment.Name);
                return enchantments;
            }
            
            //Currently only 1 enchantment is applied
            EnchantmentTemplate randomEnchantment = possibleEnchantments[UnityEngine.Random.Range(0, possibleEnchantments.Count)];
            enchantments.Add(new Enchantment(randomEnchantment.id, Random.Range(1, randomEnchantment.maxEnchantmentLevel + 1)));
            return enchantments;
        }
    }

    private static EnchantmentApplyType GetEnchantmentApplyType(Equipment equipment)
    {
        if(equipment is Weapon)
        {
            return EnchantmentApplyType.Weapon;
        }
        else if(equipment is Armor)
        {
            return EnchantmentApplyType.Armor;
        }
        else
        {
            return EnchantmentApplyType.All;
        }
    }

}