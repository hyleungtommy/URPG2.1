using UnityEngine.UI;
using UnityEngine;
public class SkillInfoPanel:InfoPanel{
    [SerializeField] Text skillNameText;
    [SerializeField] Text skillDescriptionText;
    [SerializeField] Text skillTypeText;

    public override void Render(){
        Skill selectedSkill = obj as Skill;
        skillNameText.text = selectedSkill.Name;
        skillDescriptionText.text = selectedSkill.Description.Replace("%mod%", (selectedSkill.Modifier * 100).ToString("F0") + "%");
        skillTypeText.text = FormatSkillType(selectedSkill);
    }

    private string FormatSkillType(Skill skill)
    {
        return skill.Type.ToString() + "\n" + skill.MpCost + "\n" + skill.Modifier + "\n" + skill.HitCount + "\n" + skill.Cooldown;
    }
}