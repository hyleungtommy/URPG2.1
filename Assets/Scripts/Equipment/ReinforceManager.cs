using System.Linq;
public static class ReinforceManager
{
    public static bool CanReinforce(Equipment equipment)
    {
        // an equipment can be reinforced if there is a matching criteria in ReinforceDataTemplate, and is lower than the max reinforce level
        ReinforceEquipmentType reinforceEquipmentType = GetReinforceEquipmentType(equipment);
        ReinforceDataTemplate reinforceDataTemplate = DBManager.Instance.GetAllReinforceDataTemplates().FirstOrDefault(r => r.equipmentType == reinforceEquipmentType && r.equipmentLv == equipment.RequireLv);
        if (reinforceDataTemplate == null)
        {
            return false;
        }
        else
        {
            return equipment.ReinforceLv < reinforceDataTemplate.maxReinforceLevel;
        }
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