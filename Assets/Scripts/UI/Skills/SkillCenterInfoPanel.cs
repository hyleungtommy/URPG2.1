using UnityEngine;
using UnityEngine.UI;

public class SkillCenterInfoPanel : InfoPanel
{
    [SerializeField] Text skillNameText;
    [SerializeField] Text skillDescriptionText;
    [SerializeField] Text skillPriceText;
    [SerializeField] Text skillTypeText;
    [SerializeField] Text availableSkillPointText;
    [SerializeField] Image skillIcon;
    [SerializeField] Button buySkillButton;
    [SerializeField] SkillCenterScene skillCenterScene;
    [SerializeField] Text errorText;


    public override void Render(){
        Skill selectedSkill = obj as Skill;
        skillNameText.text = selectedSkill.Name;
        skillDescriptionText.text = selectedSkill.Description.Replace("%mod%", (selectedSkill.Modifier * 100).ToString("F0") + "%");
        skillPriceText.text = selectedSkill.Price.ToString();
        skillTypeText.text = FormatSkillType(selectedSkill);
        availableSkillPointText.text = "Available Skill Point: " + skillCenterScene.selectedMember.SkillPointAvailable.ToString();
        skillIcon.sprite = selectedSkill.Icon;
        bool canLearnSkill = CanLearnSkill();
        buySkillButton.gameObject.SetActive(canLearnSkill);
        errorText.gameObject.SetActive(!canLearnSkill);
    }

    private string FormatSkillType(Skill skill)
    {
        if (skill.SkillLv == 0)
        {
            return skill.Type.ToString() + " Lv." + skill.SkillLv + " -> " + (skill.SkillLv + 1)
        + "\nMP Cost: " + 0 + " -> " + skill.MpCostNextLevel
        + "\nModifier: " + 1 + " -> " + skill.ModifierNextLevel
        + "\nHit Count: " + skill.HitCount
        + "\nCooldown: " + skill.Cooldown;
        }else{
            return skill.Type.ToString() + " Lv." + skill.SkillLv + " -> " + (skill.SkillLv + 1)
        + "\nMP Cost: " + skill.MpCost + " -> " + skill.MpCostNextLevel
        + "\nModifier: " + skill.Modifier + " -> " + skill.ModifierNextLevel
        + "\nHit Count: " + skill.HitCount
        + "\nCooldown: " + skill.Cooldown;
        }
    }

    public void OnClickBuySkill()
    {
        Skill selectedSkill = obj as Skill;
        if (CanLearnSkill())
        {
            Game.Money -= selectedSkill.Price;
            skillCenterScene.selectedMember.LearnSkill(selectedSkill);
            skillCenterScene.UpdateMoneyText();
            Render();
        }
    }

    private bool CanLearnSkill(){
        Skill selectedSkill = obj as Skill;
        if (skillCenterScene.selectedMember != null && skillCenterScene.selectedMember.SkillPointAvailable <= 0)
        {
            errorText.text = "No skill point available";
            return false;
        }
        if (Game.Money < selectedSkill.Price)
        {
            errorText.text = "Not enough money";
            return false;
        }
        return true;
    }
}