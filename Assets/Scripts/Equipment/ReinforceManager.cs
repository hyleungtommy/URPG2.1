using System.Linq;
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
}