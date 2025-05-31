using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCenterScene : MemberListScene
{
    [SerializeField] GameObject skillCenterPanelRowPrefab;
    [SerializeField] Transform skillCenterPanelRowContainer;
    [SerializeField] Text moneyText;
    [SerializeField] Text skillNameText;
    [SerializeField] Text skillDescriptionText;
    [SerializeField] Text skillPriceText;
    [SerializeField] Text skillTypeText;
    [SerializeField] Text availableSkillPointText;
    [SerializeField] Image skillIcon;
    private List<SkillCenterPanelRow> skillCenterPanelRows = new List<SkillCenterPanelRow>();
    private Skill[] skills;
    private BattleCharacter member;
    private Skill selectedSkill;

    void Start()
    {
        moneyText.text = GameController.Instance.money.ToString();
    }

    public override void OnMemberSelected(BattleCharacter member)
    {
        this.member = member;
        skills = member.CharacterClass.FullSkillList;
        foreach (Transform child in skillCenterPanelRowContainer)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < skills.Length; i++)
        {
            int j = i;
            GameObject boxObj = Instantiate(skillCenterPanelRowPrefab, skillCenterPanelRowContainer);
            SkillCenterPanelRow row = boxObj.GetComponent<SkillCenterPanelRow>();
            row.GetComponent<Button>().onClick.AddListener(() => this.OnSkillCenterPanelRowClicked(j));
            row.Render(skills[i]);
            skillCenterPanelRows.Add(row);
        }
    }

    private void OnSkillCenterPanelRowClicked(int index)
    {
        selectedSkill = skills[index];
        skillNameText.text = selectedSkill.Name;
        skillDescriptionText.text = selectedSkill.Description.Replace("%mod%", (selectedSkill.Modifier * 100).ToString("F0") + "%");
        skillPriceText.text = selectedSkill.Price.ToString();
        skillTypeText.text = FormatSkillType(selectedSkill);
        availableSkillPointText.text = "Available Skill Point: " + member.SkillPointAvailable.ToString();
        skillIcon.sprite = selectedSkill.Icon;
    }

    public void OnClickBuySkill()
    {
        if (member.SkillPointAvailable > 0 && GameController.Instance.money >= selectedSkill.Price)
        {
            member.LearnSkill(selectedSkill);
            moneyText.text = GameController.Instance.money.ToString();
            skillNameText.text = selectedSkill.Name;
            skillDescriptionText.text = selectedSkill.Description.Replace("%mod%", (selectedSkill.Modifier * 100).ToString("F0") + "%");
            skillPriceText.text = selectedSkill.Price.ToString();
            skillTypeText.text = FormatSkillType(selectedSkill);
            availableSkillPointText.text = "Available Skill Point: " + member.SkillPointAvailable.ToString();
            skillIcon.sprite = selectedSkill.Icon;
        }
    }

    public string FormatSkillType(Skill skill)
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
}
