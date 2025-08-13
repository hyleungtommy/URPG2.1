using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleSkillPanel : MonoBehaviour
{
    [SerializeField] GameObject skillPanelRowPrefab;
    [SerializeField] Transform skillPanelContent;

    private List<SkillListBox> skillPanelRows = new List<SkillListBox>();
    private Skill[] skills;

    private BattleEntity user;

    public void Open(Skill[] skills, BattleEntity user){
        this.skills = skills;
        this.user = user;
        // Clear existing boxes
        foreach (Transform child in skillPanelContent)
        {
            Destroy(child.gameObject);
        }
        skillPanelRows.Clear();

        for (int i = 0; i < skills.Length; i++)
        {
            int j = i;
            GameObject box = Instantiate(skillPanelRowPrefab, skillPanelContent);
            SkillListBox skillPanelRow = box.GetComponent<SkillListBox>();
            box.GetComponent<Button>().onClick.AddListener(() => this.OnSkillPanelRowClicked(j));
            skillPanelRow.Render(skills[i], user);
            skillPanelRows.Add(skillPanelRow);
        }

        gameObject.SetActive(true);
    }

    private void OnSkillPanelRowClicked(int index){
        Skill selectedSkill = skills[index];
        if(user.CurrentMP >= selectedSkill.MpCost){
            BattleScene.Instance.OnSelectSkill(selectedSkill);
        }
    }

    public void Close(){
        gameObject.SetActive(false);
    }
    
}