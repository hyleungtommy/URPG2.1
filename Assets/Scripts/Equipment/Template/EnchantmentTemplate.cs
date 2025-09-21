using UnityEngine;

[CreateAssetMenu(fileName = "New Enchantment", menuName = "Equipment/Enchantment")]
public class EnchantmentTemplate : ScriptableObject
{
    public int id;
    public string EnchantmentName;
    public EnchantmentType type;
    public EnchantmentApplyType applyType;
    public string description;
    public int maxEnchantmentLevel;
    public int startValue;
    public int valueIncreasePerLevel;
}

public enum EnchantmentType{
    HP, MP, ATK, DEF, MATK, MDEF, AGI, DEX
}

public enum EnchantmentApplyType
{
    All,
    Weapon,
    Armor,
    Accessory
}


