using UnityEngine;
using UnityEngine.UI;

public class QuestRequirementRow : MonoBehaviour{
    public Image RequirementIcon;
    public Text RequirementName;
    public Text RequirementProgress;
    public void Render(QuestRequirement requirement){
        if(requirement.Requirement is EnemyTemplate){
            RequirementIcon.sprite = (requirement.Requirement as EnemyTemplate).EnemyImage;
            RequirementName.text = (requirement.Requirement as EnemyTemplate).EnemyName;
        }
        if(requirement.Requirement is ItemTemplate){
            RequirementIcon.sprite = (requirement.Requirement as ItemTemplate).Icon;
            RequirementName.text = (requirement.Requirement as ItemTemplate).Name;
        }
        RequirementProgress.text = $"{requirement.CurrentAmount}/{requirement.Amount}";
    }
}