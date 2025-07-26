using UnityEngine;
using System;

[Serializable]
public class QuestRequirementTemplate
{
    public enum RequirementType
    {
        Item,
        Enemy
    }
    
    public RequirementType Type;
    public ItemTemplate ItemRequirement;
    public EnemyTemplate EnemyRequirement;
    public int Amount;
    
    public IQuestRequirement GetRequirement()
    {
        if (Type == RequirementType.Item)
            return ItemRequirement;
        return EnemyRequirement;
    }
    
    public string GetRequirementName()
    {
        if (Type == RequirementType.Item && ItemRequirement != null)
            return ItemRequirement.Name;
        if (Type == RequirementType.Enemy && EnemyRequirement != null)
            return EnemyRequirement.EnemyName;
        return "None";
    }
}