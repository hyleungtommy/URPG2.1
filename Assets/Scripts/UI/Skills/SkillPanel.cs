using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    [SerializeField] GameObject skillPanelRowPrefab;
    [SerializeField] Transform skillPanelContent;
    [SerializeField] Text skillName;
    [SerializeField] Text skillDescription;
    [SerializeField] Text skillType;
    [SerializeField] Text characterName;

    private List<SkillListBox> skillPanelRows = new List<SkillListBox>();

    private Skill[] skills;
    private BattleCharacter character;
    public void Setup(Skill[] skills, BattleCharacter character)
    {
        this.skills = skills;
        this.character = character;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        foreach (Transform child in skillPanelContent)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < skills.Length; i++)
        {
            int j = i;
            GameObject boxObj = Instantiate(skillPanelRowPrefab, skillPanelContent);
            SkillListBox row = boxObj.GetComponent<SkillListBox>();
            row.GetComponent<Button>().onClick.AddListener(() => this.OnSkillPanelRowClicked(j));
            row.Render(skills[i]);
            skillPanelRows.Add(row);
        }
        characterName.text = character.Name;
    }

    private void OnSkillPanelRowClicked(int index)
    {
        Skill selectedSkill = skills[index];
        skillName.text = selectedSkill.Name;
        skillDescription.text = selectedSkill.Description.Replace("%mod%", (selectedSkill.Modifier * 100).ToString("F0") + "%");
        skillType.text = FormatSkillType(selectedSkill);
    }

    private string FormatSkillType(Skill skill)
    {
        return skill.Type.ToString() + "\n" + skill.MpCost + "\n" + skill.Modifier + "\n" + skill.HitCount + "\n" + skill.Cooldown;
    }

}