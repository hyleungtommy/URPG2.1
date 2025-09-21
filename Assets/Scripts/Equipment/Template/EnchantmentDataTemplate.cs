using UnityEngine;

[CreateAssetMenu(fileName = "New EnchantmentData", menuName = "Equipment/EnchantmentData")]
public class EnchantmentDataTemplate : ScriptableObject
{
    public int equipmentRequireLevel;
    public CraftRequirementTemplate[] requirements;
}

