using UnityEngine;
using System;

[CreateAssetMenu(fileName = "CraftRecipeTemplate", menuName = "Craft/CraftRecipe")]
public class CraftRecipeTemplate:ScriptableObject{
 
    public CraftTemplateType type;
    public CraftRequirementTemplate[] requirements;
    public int requireSkillLevel = 1;
    public int resultAmount = 1;
    public ItemTemplate resultItem;

}

[Serializable]
public class CraftRequirementTemplate{
    public ItemTemplate item;
    public int amount = 1;
}

public enum CraftTemplateType{
    Alchemy, Weapon, Armor, Accessory
}
